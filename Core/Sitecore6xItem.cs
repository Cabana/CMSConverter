﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections.Specialized;
using System.Web;


namespace CMSConverter.Core
{
    public class Sitecore6xItem : IItem
    {
        const string TEMPLATE_IMPORT_FOLDER = "/sitecore/templates/Imported";
        const bool CREATE_NEW_BASE_TEMPLATE = false;

        private Guid _ID = Guid.Empty;
        private string _sName = "";
        private string _sKey = "";
        private string _sPath = "";
        private string _sIcon = "";
        private Guid _TemplateID = Guid.Empty;
        private string _sTemplateName = "";
        private Sitecore6xItem[] _Templates = null;
        private string[] _sTemplateIDs = null;
        private List<Sitecore6xField> _fields = new List<Sitecore6xField>();
        private Sitecore61.VisualSitecoreService _sitecoreApi = null;
        private Sitecore61.Credentials _credentials = null;
        private string _sXmlNode = "";
        private IItem _Parent = null;
        private string _sSortOrder = "";
        private bool _bHasChildren = false;

        // Global variables
        private static StringDictionary _roleNames = new StringDictionary();
        private static Sitecore6xItem _BaseTemplate = null;
        private ConverterOptions _Options = null; //new ConverterOptions();
        private static ExtendedSitecoreAPI.Security _security = null;


        public ExtendedSitecoreAPI.Security ExtendedWebService
        {
            get
            {
                if (_security == null)
                {
                    _security = new CMSConverter.Core.ExtendedSitecoreAPI.Security();
                    Uri securityUri = new Uri(_sitecoreApi.Url);
                    _security.Url = securityUri.Scheme + "://" + securityUri.Host + "/sitecore%20modules/shell/ExtendedSitecoreAPI/security.asmx";
                    // Timeout = 30 seconds (default 10)
                    _security.Timeout = 300000;
                }
                return _security;
            }
        }



        public string ID
        {
            get
            {
                return Util.GuidToSitecoreID(_ID);
            }
        }

        public string Name
        {
            get
            {
                return _sName;
            }
            set
            {
                _sName = value;
            }
        }

        public string Key
        {
            get
            {
                return _sKey;
            }
            set
            {
                _sKey = value;
            }
        }

        public string Path
        {
            get
            {
                return _sPath;
            }
            set
            {
                _sPath = value;
            }
        }

        public string Icon
        {
            get
            {
                return _sIcon;
            }
            set
            {
                _sIcon = value;
            }
        }

        public Guid TemplateID
        {
            get
            {
                return _TemplateID;
            }
        }

        public string TemplateName
        {
            get
            {
                return _sTemplateName;
            }
        }

        public string SortOrder
        {
            get
            {
                return _sSortOrder;
            }
            set
            {
                _sSortOrder = value;
            }
        }

        public IItem[] Templates
        {
            get
            {
                if (_Templates == null)
                {
                    _Templates = new Sitecore6xItem[_sTemplateIDs.Length];
                    for (int t = 0; t < _Templates.Length; t++)
                    {
                        _Templates[t] = GetSitecore61Item(_sTemplateIDs[t]);
                    }
                }
                return _Templates;
            }
        }

        public IItem BaseTemplate
        {
            get
            {
                if (_BaseTemplate == null)
                {
                    _BaseTemplate = GetSitecore61Item("/sitecore/templates/System/Templates/Standard template");
                    _BaseTemplate.Name = "Sitecore6x Standard template";
                    _BaseTemplate.Key = _BaseTemplate.Name.ToLower();
                }
                return _BaseTemplate;
                //                return GetSitecore61Item("/sitecore/templates/System/Templates/Template");
            }
        }

        public IField[] Fields
        {
            get
            {
                return _fields.ToArray();
            }
        }

        public IRole[] Roles
        {
            get
            {
                return null;
            }
        }

        public IRole[] Users
        {
            get
            {
                throw new Exception("Error Users is not implemented.");
            }
        }


        public IItem Parent
        {
            get
            {
                return _Parent;
            }
        }


        public IItem[] GetChildren()
        {
            if (_sitecoreApi == null)
                throw new Exception("sitecoreApi not set please call constructor with API object");

            XmlNode itemNode = _sitecoreApi.GetChildren("{" + _ID.ToString().ToUpper() + "}", Util.CurrentDatabase, _credentials);

            XmlNodeList nodeList = itemNode.SelectNodes("./item");
            List<Sitecore6xItem> children = new List<Sitecore6xItem>();
            foreach (XmlNode node in nodeList)
            {
                // Get full-blown item
                XmlNode item = GetItemXml(node.Attributes["id"].Value);

                // there is children
                XmlAttribute attr = item.OwnerDocument.CreateAttribute("haschildren");
                if (node.Attributes["haschildren"].Value == "1")
                    attr.Value = "1";
                else
                    attr.Value = "0";
                item.Attributes.Append(attr);
                children.Add(new Sitecore6xItem(item, this, _sitecoreApi, _credentials, _Options));

            }
            return children.ToArray();
        }

        private Sitecore6xItem(XmlNode itemNode, Sitecore6xItem parent, Sitecore61.VisualSitecoreService sitecoreApi, Sitecore61.Credentials credentials, ConverterOptions Options, bool bAllFields = false)
        {
            _Parent = parent;
            _sitecoreApi = sitecoreApi;
            _credentials = credentials;
            _Options = Options;

            _ID = new Guid(itemNode.Attributes["id"].Value);
            _sName = itemNode.Attributes["name"].Value;
            _sKey = itemNode.Attributes["key"].Value;
            _TemplateID = new Guid(itemNode.Attributes["tid"].Value);
            _sTemplateName = itemNode.Attributes["template"].Value;
            // If basetemplate isn't defined use _TemplateID otherwise extract list of inherited templates
            XmlNode baseTemplateNode = itemNode.SelectSingleNode("//field[@key='__base template']/content");
            if (baseTemplateNode == null)
            {
                _sTemplateIDs = new string[1];
                _sTemplateIDs[0] = Util.GuidToSitecoreID(_TemplateID);
            }
            else
                _sTemplateIDs = baseTemplateNode.InnerText.Split('|');

            // _sParentID = itemNode.Attributes["parentid"].Value;
            _sSortOrder = itemNode.Attributes["sortorder"].Value;
            if ((itemNode.Attributes["haschildren"] != null) && (itemNode.Attributes["haschildren"].Value == "1"))
                _bHasChildren = true;

            _sPath = "";
            string sItemVersion = GetItemVersion(itemNode);
            XmlNode contentNode = _sitecoreApi.GetItemFields(Util.GuidToSitecoreID(_ID), this.Options.Language, sItemVersion, bAllFields, Util.CurrentDatabase, _credentials);
            XmlNodeList paths = contentNode.SelectNodes("/path/item");
            foreach (XmlNode path in paths)
            {
                if (_sPath == "")
                    _sPath = "/" + path.Attributes["name"].Value;
                else
                    _sPath = "/" + path.Attributes["name"].Value + _sPath;
            }

            if (bAllFields)
            {
                foreach (XmlNode node in contentNode.SelectNodes("field"))
                {
                    Sitecore6xField field = new Sitecore6xField(node);
                    _fields.Add(field);
                }
            }

            // Add missing fields from other language versions, this is nessesary because new templates 
            // are created from an items fields.
            // The program always assumes that all fields exists, so the fieldvalues could be empty in 
            // order to prevent copying to another language.
            XmlNodeList fieldList = itemNode.SelectNodes("version[@language='" + this.Options.Language + "']/fields/field");
            foreach (XmlNode node in fieldList)
            {
                Sitecore6xField field = new Sitecore6xField(node);
                var existingfields = from f in _fields
                                     where f.TemplateFieldID == field.TemplateFieldID
                                     select f;
                if (existingfields.Count() == 0)
                {
                    if ((field.Name.ToLower() == "__icon") && (field.Content.Length > 0))
                        _sIcon = field.Content;

                    if (field.Name.ToLower() == "__sortorder")
                        _sSortOrder = field.Content;

                    if ((field.Name.ToLower() == "__display name") && (field.Content != ""))
                        _sName = field.Content;

                    // Caching of template fields items 
                    XmlNode templateFieldNode = null;
                    if (!_Options.ExistingTemplateFields.TryGetValue(field.TemplateFieldID, out templateFieldNode))
                    {
                        lock (_Options.ExistingTemplateFields)
                        {
                            try
                            {
                                templateFieldNode = GetItemXml(field.TemplateFieldID);
                                _Options.ExistingTemplateFields.Add(field.TemplateFieldID, templateFieldNode);
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.ToLower().IndexOf("object reference not set to an instance of an object.") > -1)
                                {
                                    // Do nothing this happens when there is content that is missing it's template field (probably the whole template)
                                    continue;
                                }
                                else
                                    throw new Exception("Error retrieving sitecore Field.", ex);
                            }
                        }
                    }

                    // Get name and field sortorder, unfortunately we have to retrieve the field item which is slow
                    field.SortOrder = templateFieldNode.Attributes["sortorder"].Value;
                    field.Name = templateFieldNode.Attributes["name"].Value;
                    // Get section from contentNode
                    XmlNode fieldNode = contentNode.SelectSingleNode("/field[@fieldid='" + field.TemplateFieldID + "']");
                    if ((fieldNode != null) && (fieldNode.Attributes["section"] != null))
                        field.Section = fieldNode.Attributes["section"].Value;

                    _fields.Add(field);
                }
            }

            // This is a template itemm
            if ((_sPath.ToLower().IndexOf("/sitecore/templates") > -1) && (_sName != "__Standard Values"))
            {
                XmlNode childrenNode = _sitecoreApi.GetChildren("{" + _ID.ToString().ToUpper() + "}", Util.CurrentDatabase, _credentials);

                XmlNodeList nodeList = childrenNode.SelectNodes("./item");
                foreach (XmlNode node in nodeList)
                {
                    // Get full-blown item
                    XmlNode item = GetItemXml(node.Attributes["id"].Value);
                    string sSection = "";
                    if (item.Attributes["template"].Value == "template section")
                    {
                        sSection = item.Attributes["name"].Value;

                        XmlNode sectionChildNodes = _sitecoreApi.GetChildren(node.Attributes["id"].Value, Util.CurrentDatabase, _credentials);
                        foreach (XmlNode tempNode in sectionChildNodes.SelectNodes("./item"))
                        {
                            XmlNode templateFieldNode = GetItemXml(tempNode.Attributes["id"].Value);

                            Sitecore6xField field = new Sitecore6xField(
                                        templateFieldNode.Attributes["name"].Value,
                                        templateFieldNode.Attributes["name"].Value.ToLower(),
                                        Util.GetNodeFieldValue(templateFieldNode, "//field[@key='type']/content"),
                                        new Guid(templateFieldNode.Attributes["id"].Value),
                                        "",
                                        Util.GetNodeFieldValue(templateFieldNode, "//field[@key='__sortorder']/content"),
                                        sSection);
                            field.Source = Util.GetNodeFieldValue(templateFieldNode, "//field[@key='source']/content");
                            field.LanguageTitle = Util.GetNodeFieldValue(templateFieldNode, "//field[@key='title']/content");


                            var existingfields = from f in _fields
                                                 where f.TemplateFieldID == field.TemplateFieldID
                                                 select f;
                            if (existingfields.Count() == 0)
                            {
                                _fields.Add(field);
                            }
                        }
                    }
                }

                // Fill template field values with standard values
                Sitecore6xItem standardValueItem = GetSitecore61Item(_sPath + "/__Standard Values");
                if (standardValueItem != null)
                {
                    foreach (Sitecore6xField standardField in standardValueItem._fields)
                    {
                        // Add field values from this and inherited templates
                        Sitecore6xField curField = Util.GetFieldByID(standardField.TemplateFieldID, Fields) as Sitecore6xField;

                        // Prevent lock from being copied, known to cause problems
                        if ((curField != null) && (curField.Name == "lock"))
                            continue;

                        if (curField == null)
                            _fields.Add(standardField);
                        else
                            curField.Content = standardField.Content;
                    }
                }
            }
            // This is a "__Standard Values" item for the current language layer
            else if ((_sPath.ToLower().IndexOf("/sitecore/templates") > -1) && (_sName == "__Standard Values"))
            {
            }



            _sXmlNode = itemNode.OuterXml;
        }

        public string GetOuterXml()
        {
            return _sXmlNode;
        }


        public string GetHostUrl()
        {
            return _sitecoreApi.Url;
        }


        public void SetAPIConnection(Sitecore61.VisualSitecoreService sitecoreApi, Sitecore61.Credentials credentials)
        {
            _sitecoreApi = sitecoreApi;
            _credentials = credentials;
        }

        private void CopyChildren(Sitecore6xItem parentItem, IItem CopyFrom)
        {
            if (Util.backgroundWorker.CancellationPending)
                return;

            IItem[] sourceChildren = CopyFrom.GetChildren();
            foreach (IItem child in sourceChildren)
            {
                if (Util.backgroundWorker.CancellationPending)
                    return;
                try
                {
                    Sitecore6xItem newItem = parentItem.CopyItemTo(child);

                    // recursively copy children
                    if (newItem != null)
                        CopyChildren(newItem, child);
                }
                catch (Exception ex)
                {
                    if (Util.ExceptionContainsMessage(ex, "Unknown item"))
                    {
                        Util.AddWarning("Found an 'Unknown item' at: " + child.Path + ", the item is probably missing its template.");
                        continue;
                    }
                    else
                        throw new Exception("Error while copying from: " + child.Path, ex);
                }
            }
        }

        public void CopyTo(IItem CopyFrom, bool bRecursive)
        {
            // Clear out cached list of templates and Template fields in case we are copying to a new language layer
            lock (_Options.ExistingTemplates)
            {
                _Options.ExistingTemplates.Clear();
            }
            lock (_Options.ExistingTemplateFields)
            {
                _Options.ExistingTemplateFields.Clear();
            }

            Sitecore6xItem rootItem = this.CopyItemTo(CopyFrom);
            if ((bRecursive) && (rootItem != null))
            {
                CopyChildren(rootItem, CopyFrom);
            }
        }

        public void CopyTo(IItem CopyTo, string sName)
        {
            XmlNode retVal = _sitecoreApi.CopyTo(this.ID, CopyTo.ID, sName, Util.CurrentDatabase, _credentials);
            CheckSitecoreReturnValue(retVal);
        }

        public bool MoveTo(IItem MoveTo)
        {
            XmlNode retVal = _sitecoreApi.MoveTo(this.ID, MoveTo.ID, Util.CurrentDatabase, _credentials);
            if (retVal.SelectSingleNode("status").InnerText != "ok")
                return false;
            else
            {
                // The path is only changed to visually indicate the change, no need to save again
                this.Path = MoveTo.Path + "/" + this.Name;
                return true;
            }
        }


        public void Delete()
        {
            _sitecoreApi.Delete(Util.GuidToSitecoreID(_ID), true, Util.CurrentDatabase, _credentials);
        }

        public void Save()
        {
            // Add sort order                
            AddFieldFromTemplate(this, "/sitecore/templates/System/Templates/Sections/Appearance/Appearance/__Sortorder", this.SortOrder);
            // Add icon path for templates               
            if (this.Path.ToLower().IndexOf("/sitecore/templates/") == 0)
                AddFieldFromTemplate(this, "/sitecore/templates/System/Templates/Sections/Appearance/Appearance/__Icon", this.Icon);
            // Save all fields
            this.Save(this.Fields, this.Options.Language, "1");
        }

        //  sTemplatePath = "/sitecore/templates/common/folder"
        public string AddFromTemplate(string sName, string sTemplatePath)
        {
            Sitecore6xItem template = GetSitecore61Item(sTemplatePath);
            XmlNode resultNode = _sitecoreApi.AddFromTemplate(this.ID, template.ID, sName, Util.CurrentDatabase, _credentials);
            CheckSitecoreReturnValue(resultNode);
            return resultNode.SelectSingleNode("//data/data").InnerText;
        }

        public bool HasChildren()
        {
            return _bHasChildren;
        }

        public ConverterOptions Options
        {
            get
            {
                return _Options;
            }
        }

        public string[] GetLanguages()
        {
            XmlNode languagesNode = _sitecoreApi.GetLanguages(Util.CurrentDatabase, _credentials);
            XmlNodeList languageList = languagesNode.SelectNodes("language");
            string[] result = new string[languageList.Count];
            for (int t = 0; t < languageList.Count; t++)
            {
                result[t] = languageList[t].Attributes["name"].Value;
            }
            return result;
        }

        private XmlNode CheckSitecoreReturnValue(XmlNode retVal)
        {
            if (retVal.SelectSingleNode("status").InnerText != "ok")
            {
                throw new Exception("Sitecore webservice returned " + retVal.SelectSingleNode("status").InnerText + ": " +
                                    retVal.SelectSingleNode("error").InnerText);
            }
            return retVal;
        }



        /// <summary>
        /// Change a templates inheritance from another template
        /// returns TemplateItem
        /// </summary>
        private Sitecore6xItem ChangeTemplateInheritance(Sitecore6xItem templateItem, string sTemplatePath, string sBaseTemplatePath)
        {
            // Get standard template
            XmlNode baseTemplate = GetItemXml(sBaseTemplatePath);
            string sBaseTemplateID = baseTemplate.Attributes["id"].Value;

            templateItem._fields.Add(new Sitecore6xField("__base template", "__base template", "tree list", new Guid("{12C33F3F-86C5-43A5-AEB4-5598CEC45116}"), sBaseTemplateID, null, ""));

            // Return changed template item
            return templateItem;
        }

        private Sitecore6xItem CreateItem(string sParentPath, string sFromTemplatePath, string sName)
        {
            string newItemID = "";
            CreateTemplateItemWithSpecificID(sParentPath, sFromTemplatePath, ref newItemID, sName);
            return GetSitecore61Item(newItemID);
        }

        // Same as below but without ref toTemplateID
        public string CreateTemplateItemWithSpecificID(string sParentPath, string sFromTemplatePath, string toTemplateID, string sName)
        {
            return CreateTemplateItemWithSpecificID(sParentPath, sFromTemplatePath, ref toTemplateID, sName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sParentPath">Path of parent item</param>
        /// <param name="sFromTemplatePath">Path to template that the new item will be created from</param>
        /// <param name="toTemplateID">ID of new item</param>
        /// <param name="sName">Name of new item</param>
        /// <returns></returns>
        private string CreateTemplateItemWithSpecificID(string sParentPath, string sFromTemplatePath, ref string toTemplateID, string sName)
        {
            // Get standard template
            XmlNode standardTemplate = GetItemXml(sFromTemplatePath);
            string sStandardTemplateID = standardTemplate.Attributes["id"].Value;
            string sItemName = Util.MakeValidNodeName(sName);

            // Create new template from the standard template
            XmlNode resultNode = CheckSitecoreReturnValue(_sitecoreApi.AddFromTemplate(sParentPath, sStandardTemplateID, sItemName, Util.CurrentDatabase, _credentials));

            // Only set template id if one is given
            if (toTemplateID != "")
            {
                string sNewTemplateID = resultNode.SelectSingleNode("//data/data").InnerText;
                // Get newly created template, which will be used as a temporary template
                XmlNode newTemplateNode = GetItemXml(sNewTemplateID);
                // Modify it's ID 
                newTemplateNode.Attributes["id"].Value = toTemplateID;

                // Remove temporary template
                _sitecoreApi.Delete(sNewTemplateID, false, Util.CurrentDatabase, _credentials);

                // Create new template with the source (CopyFrom template) id
                _sitecoreApi.InsertXML(sParentPath, newTemplateNode.OuterXml, false, Util.CurrentDatabase, _credentials);
            }
            else
            {
                string sNewTemplateID = resultNode.SelectSingleNode("//data/data").InnerText;
                toTemplateID = sNewTemplateID;
            }
            return sParentPath + "/" + sItemName;
        }


        /// <summary>
        /// Create template from standard template
        /// </summary>
        private string CreateTemplate(string sParentPath, string sTemplateName)
        {
            return CreateTemplateItemWithSpecificID(sParentPath, "/sitecore/templates/System/Templates/Template", "", sTemplateName);
        }


        /// <summary>
        /// Create Template from any given template
        /// </summary>
        private string CreateTemplate(string sParentPath, string sFromTemplatePath, string sTemplateName)
        {
            return CreateTemplateItemWithSpecificID(sParentPath, sFromTemplatePath, "", sTemplateName);
        }

        private XmlNode GetItemXml(string sPath)
        {
            XmlNode result = CheckSitecoreReturnValue(
                                _sitecoreApi.GetXML(sPath, false, Util.CurrentDatabase, _credentials));
            result = result.SelectSingleNode("//item");
            return result;
        }

        private string GetItemAttribute(string sPath, string sAttribute)
        {
            XmlNode itemNode = _sitecoreApi.GetXML(sPath, false, Util.CurrentDatabase, _credentials);
            itemNode = itemNode.SelectSingleNode("//item");
            if (itemNode == null)
                return null;
            else
                return itemNode.Attributes[sAttribute].Value;
        }


        /// <summary>
        /// Creates a new content or template field
        /// </summary>
        private XmlNode AddFieldToItem(XmlNode item, string sTfid, string sKey, string sType, string sContent)
        {
            XmlNode fields = item.SelectSingleNode("//item/version[@language = '" + this.Options.Language + "']/fields");
            XmlElement fieldElem = item.OwnerDocument.CreateElement("field");
            fieldElem.SetAttribute("tfid", sTfid);
            fieldElem.SetAttribute("key", sKey);
            fieldElem.SetAttribute("type", sType);
            fields.AppendChild(fieldElem);
            XmlElement contentElem = item.OwnerDocument.CreateElement("content");
            contentElem.InnerText = sContent;
            fieldElem.AppendChild(contentElem);
            return item;
        }

        /// <summary>
        /// Update existing item field
        /// </summary>
        private XmlNode ModifyItemField(XmlNode item, string sTfid, string sKey, string sType, string sContent)
        {
            XmlNode fieldNode = item.SelectSingleNode("//item/version[@language = '" + this.Options.Language + "']/fields/field[@tfid = '" + sTfid + "']/content");
            if (fieldNode != null)
            {
                fieldNode.InnerText = sContent;
            }
            else
            {
                item = AddFieldToItem(item, sTfid, sKey, sType, sContent);
            }
            return item;

        }

        private Sitecore6xItem AddFieldFromTemplate(Sitecore6xItem templateFieldItem, string sFieldTemplate, string sContent)
        {
            string sName = sFieldTemplate.Remove(0, sFieldTemplate.LastIndexOf("/") + 1);
            string sKey = sFieldTemplate.Remove(0, sFieldTemplate.LastIndexOf("/") + 1).ToLower();
            // Field already exist, just replace content
            IField existingField = Util.GetFieldByName(sName, templateFieldItem.Fields);
            if (existingField != null)
                existingField.Content = sContent;
            else
            {
                // Get sType field template
                XmlNode field = GetItemXml(sFieldTemplate);
                string sTemplateFieldID = field.Attributes["id"].Value;
                string sfieldTypename = field.SelectSingleNode("//item/version[@language = '" + this.Options.Language + "']/fields/field[@key='type']").InnerText;

                templateFieldItem._fields.Add(new Sitecore6xField(sName, sKey, sfieldTypename, new Guid(sTemplateFieldID), sContent, null, ""));
            }

            return templateFieldItem;
        }


        /// <summary>
        /// Adds a new field to a template using the IField fromField object
        /// IMPORTANT!! The parameter sTemplatePath should always be a valid path, never an ID.
        /// </summary>
        private void AddTemplateField(string sTemplatePath, IField fromField)
        {
            // Get "Template section" template
            XmlNode templateSection = GetItemXml("/sitecore/templates/System/Templates/Template section");
            string templateSectionID = templateSection.Attributes["id"].Value;

            string templateSectionName = "Data";
            if ((fromField.Section != "") && (fromField.Section != null))
                templateSectionName = fromField.Section;

            string dataSectionID = GetItemAttribute(sTemplatePath + "/" + templateSectionName, "id");
            if (dataSectionID == null)
            {
                // Create new "Template section" item from the "Template section" template
                XmlNode resultNode = _sitecoreApi.AddFromTemplate(sTemplatePath, templateSectionID, templateSectionName, Util.CurrentDatabase, _credentials);
                CheckSitecoreReturnValue(resultNode);
                dataSectionID = resultNode.SelectSingleNode("//data/data").InnerText;
            }

            // Create new field below Section
            CreateTemplateItemWithSpecificID(dataSectionID, "/sitecore/templates/System/Templates/Template field", fromField.TemplateFieldID, fromField.Name);

            Sitecore6xItem templateFieldItem = GetSitecore61Item(fromField.TemplateFieldID);

            // Add "Sort order" to template
            AddFieldFromTemplate(templateFieldItem, "/sitecore/templates/System/Templates/Sections/Appearance/Appearance/__Sortorder", fromField.SortOrder);

            // Add "Field type" to template
            string sFromFieldTranslated = Util.TranslateToNewFieldTypes(fromField.Type);
            AddFieldFromTemplate(templateFieldItem, "/sitecore/templates/System/Templates/Template field/Data/Type", sFromFieldTranslated);

            // Add "Field source" to template
            AddFieldFromTemplate(templateFieldItem, "/sitecore/templates/System/Templates/Template field/Data/Source", fromField.Source);

            // Add "LanguageTitle" to template
            AddFieldFromTemplate(templateFieldItem, "/sitecore/templates/System/Templates/Template field/Data/Title", fromField.LanguageTitle);

            templateFieldItem.Save(templateFieldItem.Fields, this.Options.Language, "1");
        }


        private string CreateTemplate(IItem fromTemplate, string sTemplatePath, string sInheritedTemplateIDs)
        {
            Sitecore6xItem templateItem = GetSitecore61Item(sTemplatePath);
            if (templateItem == null)
            {
                if (sTemplatePath.ToLower().IndexOf("/sitecore/templates/system/") > -1)
                    throw new Exception("'/sitecore/templates/system/xxx' templates must not be written to! This template path is: " + sTemplatePath);

                // Create new template
                if (fromTemplate.ID == this.BaseTemplate.ID)
                    // Never create a template with the same id as the base template, as this will cause the "standard template" to be overwritten
                    sTemplatePath = CreateTemplateItemWithSpecificID(fromTemplate.Path.Remove(fromTemplate.Path.LastIndexOf("/") + 1), "/sitecore/templates/System/Templates/Template", "", fromTemplate.Name);
                else
                {
                    if (CREATE_NEW_BASE_TEMPLATE)                        
                        sTemplatePath = CreateTemplateItemWithSpecificID(TEMPLATE_IMPORT_FOLDER, "/sitecore/templates/System/Templates/Template", fromTemplate.ID, fromTemplate.Name);
                    else
                    {
                        string sFolder = fromTemplate.Path.Remove(fromTemplate.Path.LastIndexOf("/") + 1);
                        Sitecore6xItem folderItem = GetSitecore61Item(sFolder);
                        // Folders to the template are missing
                        if (folderItem == null)
                        {
                            if (sFolder.IndexOf("/sitecore/templates/") == -1)
                                throw new Exception("Error template source path not in /sitecore/templates/, the illegal path was:" + sFolder);
                            // Create all missing template folders
                            string sTempName = sFolder.Remove( 0, "/sitecore/templates/".Length);
                            string sMissingFolder = "/sitecore/templates/";
                            while (sTempName != "")
                            {
                                string sMissingFolderName = sTempName.Remove(sTempName.IndexOf("/"), sTempName.Length - sTempName.IndexOf("/"));
                                folderItem = GetSitecore61Item(sMissingFolder + sMissingFolderName);
                                if (folderItem == null)
                                    CreateTemplate(sMissingFolder, "/sitecore/templates/System/Templates/Template Folder", sMissingFolderName);

                                sMissingFolder = sMissingFolder + sMissingFolderName + "/";
                                sTempName = sTempName.Remove(0, sTempName.IndexOf("/") + 1);
                            }
                        }
                        sTemplatePath = CreateTemplateItemWithSpecificID(fromTemplate.Path.Remove(fromTemplate.Path.LastIndexOf("/") + 1), "/sitecore/templates/System/Templates/Template", fromTemplate.ID, fromTemplate.Name);
                    }
                }

                

                // Get new templateItem
                templateItem = GetSitecore61Item(sTemplatePath);

                // Add icon path                
                AddFieldFromTemplate(templateItem, "/sitecore/templates/System/Templates/Sections/Appearance/Appearance/__Icon", fromTemplate.Icon);

                // Change template inheritance - MUST BE AFTER ADDING FIELD VALUES OTHERWISE IT DOESN'T WORK
                if (sInheritedTemplateIDs != "")
                    templateItem._fields.Add(new Sitecore6xField("__base template", "__base template", "tree list", new Guid("{12C33F3F-86C5-43A5-AEB4-5598CEC45116}"), sInheritedTemplateIDs, null, ""));

                templateItem.Save(templateItem.Fields, this.Options.Language, "1");

                // Create all template fields
                foreach (IField field in fromTemplate.Fields)
                {
                    // It is better to check for missing fields using id, because then it will also be found even if it is placed elsewhere                    
                    if (! Util.IsTemplateFieldOnIgnoreList(field.TemplateFieldID.ToString()) &&
                       (GetItem(field.TemplateFieldID) == null))
                        AddTemplateField(sTemplatePath, field);
                }

                // Create standard values
                if (templateItem.Path.IndexOf("/sitecore/templates/System") == -1)
                {
                    bool bContainsStandardValues = false;
                    foreach (IField field in fromTemplate.Fields)
                    {
                        if (field.Content != "")
                            bContainsStandardValues = true;
                    }
                    if (bContainsStandardValues)
                    {
                        ExtendedSitecoreAPI.Credentials credential = new ExtendedSitecoreAPI.Credentials();
                        credential.UserName = _credentials.UserName;
                        credential.Password = _credentials.Password;
                        try
                        {
                            string sID = ExtendedWebService.CreateStandardValues(templateItem.ID, credential);

                            Sitecore6xItem standardValuesItem = templateItem.GetSitecore61Item(sID);
                            //                Sitecore6xItem standardValuesItem = templateItem.GetSitecore61Item(templateItem.Path + "/__Standard Values");
                            // standardValuesItem = CreateItem(templateItem.Path, Util.GuidToSitecoreID(templateItem.ID), "__Standard Values");
                            CheckSitecoreReturnValue(_sitecoreApi.AddVersion(
                                            standardValuesItem.ID, this.Options.Language, Util.CurrentDatabase, _credentials));
                            standardValuesItem.Save(fromTemplate.Fields, this.Options.Language, "1.0");
                        }
                        catch (Exception ex)
                        {
                            Util.AddWarning("Standard values exception in Sitecore6xItem: " + ex.Message + "\n" + ex.StackTrace);
                        }
                    }
                }


                // Set sortorder on sections
                {
                    int t = 10;
                    // Basetemplate section sortorder begins from 1000
                    if ((fromTemplate.BaseTemplate != null) && (templateItem.Key == fromTemplate.BaseTemplate.Key))
                        t = 1000;
                    IItem[] childSections = templateItem.GetChildren();
                    foreach (IItem section in childSections)
                    {
                        if (section.Templates[0] == null)
                            continue;

                        if (section.Templates[0].Key == "template section")
                        {
                            if (section.SortOrder != t.ToString())
                            {
                                section.SortOrder = t.ToString();
                                section.Save();
                            }
                            // Basetemplate section sortorder is increased by 1000
                            if ((fromTemplate.BaseTemplate != null) && (templateItem.Key == fromTemplate.BaseTemplate.Key))
                                t += 1000;
                            else
                                t += 10;
                        }
                    }
                }

                // Call plugins and update user interface
                if (this.Options.CopyItem != null)
                {
                    //                    templateItem = GetSitecore61Item(sTemplatePath);
                    this.Options.CopyItem(fromTemplate, templateItem.Parent, templateItem);
                    templateItem.Save();
                }
            }
            // Template already exists, but it is not in the cache
            else if (!_Options.ExistingTemplates.ContainsKey(templateItem.ID.ToLower()))
            {
                // Does all the fields exist?
                foreach (IField field in fromTemplate.Fields)
                {
                    // A new field exists
                    if (Util.GetFieldByID(field.TemplateFieldID, templateItem.Fields) == null)
                    {
                        // Create and add it to the template
                        if (! Util.IsTemplateFieldOnIgnoreList(field.TemplateFieldID) &&
                           (GetItem(field.TemplateFieldID) == null))
                        {
                            AddTemplateField(templateItem.Path, field);
                            Util.AddWarning("Added new field to template: " + templateItem.Path + "\nField name:" + field.Name);
                        }
                    }
                }
            }
            // Always cache the template
            AddItemToCache(templateItem);

            return sTemplatePath;
        }

        private void RecursivelyCreateTemplates(IItem CopyFrom, out string sInheritedTemplateIDs, string sBaseTemplatePath)
        {
            sInheritedTemplateIDs = sBaseTemplatePath;
            if (CopyFrom.Templates[0] != null)
            {
                foreach (IItem fromTemplate in CopyFrom.Templates)
                {
                    if ((fromTemplate.Templates == null) || (fromTemplate.Templates[0] == null))
                    {
                        // Create templates with only the standard template
                        CreateTemplate(fromTemplate, fromTemplate.ID, sBaseTemplatePath);
                        continue;
                    }

                    string sLocalInheritedTemplateIDs = "";
                    if ((fromTemplate.Templates.Length == 1) && (fromTemplate.Templates[0].Key == "standard template"))
                        sLocalInheritedTemplateIDs = fromTemplate.Templates[0].ID;
                    //                        sLocalInheritedTemplateIDs = sBaseTemplatePath;
                    else
                        RecursivelyCreateTemplates(fromTemplate, out sLocalInheritedTemplateIDs, sBaseTemplatePath);

                    CreateTemplate(fromTemplate, fromTemplate.ID, sLocalInheritedTemplateIDs);
                    if (sInheritedTemplateIDs != "")
                        sInheritedTemplateIDs += "|" + fromTemplate.ID;
                    else
                        sInheritedTemplateIDs = fromTemplate.ID;
                }
            }
        }

        private string GetItemVersion(XmlNode contentItem)
        {
            XmlNodeList versionNodes = contentItem.SelectNodes("//item/version[@language = '" + this.Options.Language + "']");

            int iVersion = -1;
            for (int t = 0; t < versionNodes.Count; t++)
            {
                int iTmpVer = -1;
                int.TryParse(versionNodes[t].Attributes["version"].Value, out iTmpVer);
                if (iVersion < iTmpVer)
                    iVersion = iTmpVer;
            }
            return iVersion.ToString();
        }


        private Sitecore6xItem CopyItemTo(IItem CopyFrom)
        {
            Sitecore6xItem returnItem = null;

            Util.SetStatus("Copying from: " + CopyFrom.Path, "Copying to: " + _sPath);

            // *** Create templates ***
            string sTemplatePath = null;

            // Don't create templates if we are using names instead of id's
            if (this.Options.CopyOperation == CopyOperations.UseNames)
            {
                sTemplatePath = CopyFrom.Templates[0].Path;
            }
            else
            {
                if (CREATE_NEW_BASE_TEMPLATE)
                {
                    // Check to see if Template import folder has been created
                    Sitecore6xItem templateFolder = GetSitecore61Item(TEMPLATE_IMPORT_FOLDER);
                    if (templateFolder == null)
                    {
                        CreateTemplate(TEMPLATE_IMPORT_FOLDER.Remove(TEMPLATE_IMPORT_FOLDER.LastIndexOf("/")),
                                        "/sitecore/templates/common/folder",
                                        TEMPLATE_IMPORT_FOLDER.Remove(0, TEMPLATE_IMPORT_FOLDER.LastIndexOf("/") + 1));
                    }
                    else
                        AddItemToCache(templateFolder);
                }


                // *** Find and create base template from current item ***
                string sBaseTemplatePath = "";
                if (CopyFrom.BaseTemplate != null)
                {
                    Sitecore6xItem baseTemplateItem = null;
                    if (CREATE_NEW_BASE_TEMPLATE)
                    {
                        // Get alternate base template from name, because we don't want to use the "normal" standard template 
                        sBaseTemplatePath = TEMPLATE_IMPORT_FOLDER + "/" + CopyFrom.BaseTemplate.Name;
                        baseTemplateItem = GetSitecore61Item(sBaseTemplatePath);
                    }
                    else
                    {
                        // Get normal standard template
                        baseTemplateItem = GetSitecore61Item(CopyFrom.BaseTemplate.ID);
                        if (baseTemplateItem != null)
                            sBaseTemplatePath = baseTemplateItem.ID;
                    }

                    if (baseTemplateItem != null)                    
                        AddItemToCache(baseTemplateItem);
                }


                // *** Create Template from current item ***
                string sInheritedTemplateIDs = "";
                // Create the templates inherited templates
                RecursivelyCreateTemplates(CopyFrom, out sInheritedTemplateIDs, sBaseTemplatePath);

                // Finally create the items template
                sTemplatePath = CopyFrom.Templates[0].ID;
                //            sTemplatePath = CreateTemplate(CopyFrom.Templates[0], Util.GuidToSitecoreID(CopyFrom.Templates[0].ID), sInheritedTemplateIDs);

                // Normal items only has one template, sTemplatePath will point to the right template.
                // The current item must be a template item because there is more than one template, so the 
                // "standard template" is used as "base template" for this template item.
                if (CopyFrom.Templates.Length > 1)
                {
                    sTemplatePath = CopyFrom.BaseTemplate.ID;
                }
            }


            // *** Create Item ***
            string sItemVersion = "1";
            string sNewItemID = "";
            Sitecore6xItem templateItem = null;
            if (this.Options.CopyOperation == CopyOperations.Overwrite)
            {
                // Find existing item
                returnItem = GetSitecore61Item(CopyFrom.ID, this);
                if ((returnItem == null) && (!Util.IsTemplateOnIgnoreList(CopyFrom.Templates)))
                {
                    // No item was found, create new using existing item id's 
                    CreateTemplateItemWithSpecificID(this.ID, sTemplatePath, CopyFrom.ID, CopyFrom.Name);
                }
                sNewItemID = CopyFrom.ID;
            }
            // Generate new items id's 
            else if (this.Options.CopyOperation == CopyOperations.GenerateNewItemIDs)
            {
                sNewItemID = "";
                CreateTemplateItemWithSpecificID(this.ID, sTemplatePath, ref sNewItemID, CopyFrom.Name);
            }
            // Skip existing items id's
            else if (this.Options.CopyOperation == CopyOperations.SkipExisting)
            {
                returnItem = GetSitecore61Item(CopyFrom.ID, this);
                if (returnItem == null)
                {
                    // Use existing item id's 
                    CreateTemplateItemWithSpecificID(this.ID, sTemplatePath, CopyFrom.ID, CopyFrom.Name);
                    sNewItemID = CopyFrom.ID;
                }
            }
            // Use names to identify items, instead of ID's
            else if (this.Options.CopyOperation == CopyOperations.UseNames)
            {
                // If the template cannot be found then skip this item
                templateItem = GetSitecore61Item(sTemplatePath);
                if (templateItem == null)
                    returnItem = null;
                else
                {
                    // Find existing item
                    returnItem = GetSitecore61Item(this.Path + "/" + CopyFrom.Name, this);
                    if (returnItem != null)
                        sNewItemID = returnItem.ID;
                    else
                    {
                        sNewItemID = "";
                        CreateTemplateItemWithSpecificID(this.ID, sTemplatePath, ref sNewItemID, CopyFrom.Name);
                    }
                }
            }


            // If we are skipping existing then do nothing here
            if ((returnItem != null) &&
                ((this.Options.CopyOperation == CopyOperations.SkipExisting) /* ||
                 (this.Options.CopyOperation == CopyOperations.UseNames) */))
            { }
            else if ((templateItem == null) &&
                (this.Options.CopyOperation == CopyOperations.UseNames))
            {
                returnItem = GetSitecore61Item(this.Path + "/" + CopyFrom.Name);
            }
            else if (Util.IsTemplateOnIgnoreList(CopyFrom.Templates))
            {
                // skip this
            }
            // Otherwise populate data fields
            else
            {
                XmlNode contentItem = GetItemXml(sNewItemID);

                // Set language
                XmlNodeList versionNodes = contentItem.SelectNodes("//item/version[@language = '" + this.Options.Language + "']");
                if ((versionNodes == null) || (versionNodes.Count == 0))
                {
                    // If language version is missing, we create it
                    CheckSitecoreReturnValue(_sitecoreApi.AddVersion(sNewItemID, this.Options.Language, Util.CurrentDatabase, _credentials));
                    contentItem = GetItemXml(sNewItemID);
                }
                else
                {
                    sItemVersion = GetItemVersion(contentItem);
                }

                // Save item by creating a new or overwrite existing
                if (returnItem == null)
                {
                    // Update existing fields
                    foreach (IField field in CopyFrom.Fields)
                    {
                        contentItem = ModifyItemField(contentItem, field.TemplateFieldID, field.Key, field.Type, field.Content);
                    }

                    CheckSitecoreReturnValue(_sitecoreApi.InsertXML(this.ID, contentItem.OuterXml, false, Util.CurrentDatabase, _credentials));

                    // Should get returnItem even though it already existed, the fields might have been updated
                    if (this.Options.CopyOperation == CopyOperations.UseNames)
                        returnItem = GetSitecore61Item(sNewItemID, this, true);
                    else
                        returnItem = GetSitecore61Item(sNewItemID, this);
                }


                // Create "Sortorder" field (actually stored on item as an attribute)
                AddFieldFromTemplate(returnItem, "/sitecore/templates/System/Templates/Sections/Appearance/Appearance/__Sortorder", CopyFrom.SortOrder);
                returnItem.SortOrder = CopyFrom.SortOrder;

                // Copy all the fields
                foreach (IField fromField in CopyFrom.Fields)
                {
                    // Fetch field using Key this is neccesary because the field constructor only reads the key, not the name
                    IField toField = Util.GetFieldByName(fromField.Key, returnItem.Fields);
                    if (toField == null)
                    {
                        Sitecore6xField field = new Sitecore6xField(fromField.Name, fromField.Key,
                                    fromField.Type, new Guid(fromField.TemplateFieldID), fromField.Content, fromField.SortOrder, fromField.Section);
                        returnItem._fields.Add(field);
                        toField = field;
                    }
                    toField.Content = ConvertFieldContent(CopyFrom, fromField);
                }

                // Call plugins and update user interface
                if (this.Options.CopyItem != null)
                {
                    this.Options.CopyItem(CopyFrom, this, returnItem);
                }

                // Save item by overwriting existing, neccessary if any returnItem fields have been changed
                returnItem.Save();

                // Copy security rights - if they exist
                if ((this.Options.CopySecuritySettings) && (CopyFrom.Roles != null))
                {
                    try
                    {
                        ExtendedSitecoreAPI.Credentials credential = new ExtendedSitecoreAPI.Credentials();
                        credential.UserName = _credentials.UserName;
                        credential.Password = _credentials.Password;


                        if ((!this.Options.UsersCopied) && (CopyFrom.Users != null))
                        {
                            for (int t = 0; t < CopyFrom.Users.Count(); t++)
                            {
                                bool bIsAdmin = false;
                                if (CopyFrom.Users[t].UserSettings["IsAdmin"].ToLower() == "true")
                                    bIsAdmin = true;
                                ExtendedWebService.CreateUser(
                                            CopyFrom.Users[t].Name,
                                            CopyFrom.Users[t].UserSettings["PassWord"],
                                            CopyFrom.Users[t].UserSettings["Email"],
                                            CopyFrom.Users[t].UserSettings["FullName"],
                                            CopyFrom.Users[t].UserSettings["Roles"],
                                            bIsAdmin,
                                            credential);

                            }
                            this.Options.UsersCopied = true;
                        }


                        for (int t = 0; t < CopyFrom.Roles.Count(); t++)
                        {
                            string sRole = CopyFrom.Roles[t].Name;
                            sRole = Util.MakeValidRoleName(sRole);

                            //                            if (sRole == "__Everyone")
                            //                                sRole = "Everyone";

                            // Add domain name to role, if it isn't already a part of a domain
                            if ((sRole.IndexOf("\\") == -1) && (this.Options.DefaultSecurityDomain != ""))
                                sRole = this.Options.DefaultSecurityDomain + "\\" + sRole;

                            // Allow
                            string sRight = "";
                            if ((CopyFrom.Roles[t].AccessRight & AccessRights.Read) == AccessRights.Read)
                                sRight += "item:read|";

                            if ((CopyFrom.Roles[t].AccessRight & AccessRights.Write) == AccessRights.Write)
                                sRight += "item:write|";

                            if ((CopyFrom.Roles[t].AccessRight & AccessRights.Create) == AccessRights.Create)
                                sRight += "item:create|";

                            if ((CopyFrom.Roles[t].AccessRight & AccessRights.Delete) == AccessRights.Delete)
                                sRight += "item:delete|";

                            if ((CopyFrom.Roles[t].AccessRight & AccessRights.Rename) == AccessRights.Rename)
                                sRight += "item:rename|";

                            if ((CopyFrom.Roles[t].AccessRight & AccessRights.Administer) == AccessRights.Administer)
                                sRight += "item:admin|";

                            // Deny
                            string sDenyRight = "";
                            if ((CopyFrom.Roles[t].AccessRight & AccessRights.DenyRead) == AccessRights.DenyRead)
                                sDenyRight += "item:read|";

                            if ((CopyFrom.Roles[t].AccessRight & AccessRights.DenyWrite) == AccessRights.DenyWrite)
                                sDenyRight += "item:write|";

                            if ((CopyFrom.Roles[t].AccessRight & AccessRights.DenyCreate) == AccessRights.DenyCreate)
                                sDenyRight += "item:create|";

                            if ((CopyFrom.Roles[t].AccessRight & AccessRights.DenyDelete) == AccessRights.DenyDelete)
                                sDenyRight += "item:delete|";

                            if ((CopyFrom.Roles[t].AccessRight & AccessRights.DenyRename) == AccessRights.DenyRename)
                                sDenyRight += "item:rename|";

                            if ((CopyFrom.Roles[t].AccessRight & AccessRights.DenyAdminister) == AccessRights.DenyAdminister)
                                sDenyRight += "item:admin|";

                            // Get existing rights and subtract them from the ones we want to set
                            if (! this.Options.SetItemRightsExplicitly)
                            {
                                string sExistingRights = ExtendedWebService.GetRight(Util.CurrentDatabase, returnItem.ID, CopyFrom.Roles[t].Name,
                                                            CMSConverter.Core.ExtendedSitecoreAPI.SecurityPermission.AllowAccess, credential);
                                sRight = Util.SubtractRights(sExistingRights, sRight);

                                string sExistingDenyRights = ExtendedWebService.GetRight(Util.CurrentDatabase, returnItem.ID, CopyFrom.Roles[t].Name,
                                                            CMSConverter.Core.ExtendedSitecoreAPI.SecurityPermission.DenyAccess, credential);
                                sDenyRight = Util.SubtractRights(sExistingDenyRights, sDenyRight);
                            }

                            // This is a user
                            if (CopyFrom.Roles[t].UserSettings.Count > 0)
                            {
                                bool bIsAdmin = false;
                                if (CopyFrom.Roles[t].UserSettings["IsAdmin"].ToLower() == "true")
                                    bIsAdmin = true;

                                ExtendedWebService.CreateUser(
                                            CopyFrom.Roles[t].Name,
                                            CopyFrom.Roles[t].UserSettings["PassWord"],
                                            CopyFrom.Roles[t].UserSettings["Email"],
                                            CopyFrom.Roles[t].UserSettings["FullName"],
                                            CopyFrom.Roles[t].UserSettings["Roles"],
                                            bIsAdmin,
                                            credential);
                            }
                            // This is a role
                            else
                            {
                                ExtendedWebService.CreateRole(sRole, credential);
                                // This role should inherit from another rootrole
                                if ((!_roleNames.ContainsKey(sRole)) && (this.Options.RootRole != ""))
                                {
                                    string sRootRole = this.Options.RootRole;
                                    if (this.Options.DefaultSecurityDomain != "")
                                        sRootRole = this.Options.DefaultSecurityDomain + "\\" + sRootRole;
                                    ExtendedWebService.AddRoleToRole(sRole, sRootRole, credential);

                                    _roleNames.Add(sRole, sRole);
                                }
                            }

                            // We also need to set rights
                            if (sRight != "")
                            {
                                ExtendedWebService.SetRight(Util.CurrentDatabase,
                                            returnItem.ID, sRole, sRight,
                                            CMSConverter.Core.ExtendedSitecoreAPI.AccessPermission.Allow,
                                            CMSConverter.Core.ExtendedSitecoreAPI.PropagationType.Any,
                                            credential);
                            }
                            if (sDenyRight != "")
                            {
                                ExtendedWebService.SetRight(Util.CurrentDatabase,
                                            returnItem.ID, sRole, sDenyRight,
                                            CMSConverter.Core.ExtendedSitecoreAPI.AccessPermission.Deny,
                                            CMSConverter.Core.ExtendedSitecoreAPI.PropagationType.Any,
                                            credential);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Options.CopySecuritySettings = false;
                        Util.AddWarning("An exception occured while transferring security settings, copying security is now disabled.");
                        Util.AddWarning("The security exception was: " + ex.Message + "\n" + ex.StackTrace);
                    }
                }
            }
            return returnItem;
        }

        /// <summary>
        /// Overwrite existing item values, item must already exist
        /// </summary>
        public void Save(IField[] fromFields, string sLanguage, string sVersion)
        {
            // Create special XmlDocument that _sitecoreApi.Save() expects, for some 
            // weird reason this is not the same as _sitecoreApi.InsertXml().
            XmlDocument doc = new XmlDocument();
            XmlElement sitecoreElem = doc.CreateElement("sitecore");
            doc.AppendChild(sitecoreElem);

            // Update existing fields
            foreach (IField field in fromFields)
            {
                XmlElement fieldElem = doc.CreateElement("field");
                fieldElem.SetAttribute("itemid", this.ID);
                fieldElem.SetAttribute("language", sLanguage);
                fieldElem.SetAttribute("version", sVersion);
                fieldElem.SetAttribute("fieldid", field.TemplateFieldID);
                sitecoreElem.AppendChild(fieldElem);

                XmlNode valueNode = doc.CreateElement("value");
                valueNode.InnerText = field.Content;
                fieldElem.AppendChild(valueNode);

            }
            // Save item 
            CheckSitecoreReturnValue(_sitecoreApi.Save(doc.OuterXml, Util.CurrentDatabase, _credentials));
        }

        /// <summary>
        /// Convert field content into sitecore 6.x format, mostly links
        /// </summary>
        private string ConvertFieldContent(IItem CopyFrom, IField fromField)
        {
            string sContent = fromField.Content;
            if ((fromField.Type == "Rich Text") || (fromField.Type == "html") ||
                (fromField.Type == "MultiLink") || (fromField.Type == "General Link") ||
                (fromField.Type == "link"))
            {
                try
                {
                    XmlDocument doc = Sgml.SgmlUtil.ParseHtml(sContent);

                    List<XmlNode> removeableNodes = new List<XmlNode>(); 
                    bool bModified = false;
                    // Convert links to new format
                    XmlNodeList linkNodes = doc.SelectNodes("//a");
                    foreach (XmlNode node in linkNodes)
                    {
                        int t = 0;
                        if (fromField.Type == "link")
                            t++;
                        string sUrl = Util.GetAttributeValue(node.Attributes["href"]);
                        string sTitle = Util.GetAttributeValue(node.Attributes["sc_text"]);
                        string sc_linktype = Util.GetAttributeValue(node.Attributes["sc_linktype"]);
                        string sc_url = Util.GetAttributeValue(node.Attributes["sc_url"]);
                        string sTarget = Util.GetAttributeValue(node.Attributes["target"]);
                        // Possible linktypes: internal, external, mailto, anchor, javascript, media
                        if (sc_linktype == "internal")
                        {
                            IItem linkItem = CopyFrom.GetItem(sc_url);
                            if (linkItem != null)
                            {                                
                                sUrl = "~/link.aspx?_id=" + new Guid(linkItem.ID).ToString("N").ToUpper() + "&_z=z";
                            }                        
                        }

                        if (sUrl == "")
                        {
                            removeableNodes.Add(node);
                            continue;
                        }

                        node.Attributes["href"].Value = sUrl;
                        node.Attributes.Remove(node.Attributes["sc_text"]);
                        node.Attributes.Remove(node.Attributes["sc_linktype"]);
                        node.Attributes.Remove(node.Attributes["sc_url"]);
                        if (node.Attributes["sc_anchor"] != null)
                            node.Attributes.Remove(node.Attributes["sc_anchor"]);
                        bModified = true;
                    }

                    foreach (XmlNode node in removeableNodes)
                    {
                        node.ParentNode.RemoveChild(node);
                    }

                    if (bModified)
                    {
                        XmlNode root = doc.SelectSingleNode("root");
                        sContent = root.InnerXml;
                        // No tags at all, add a paragraph tag
                        if (root.ChildNodes.Count == 1)
                            sContent = "<p>" + root.InnerXml + "</p>";
                    }
                    // Add paragraph tags around content that weren't modified, and haven't got any paragraph tags
                    else if (((fromField.Type == "Rich Text") || (fromField.Type == "html")) &&
                             (sContent != ""))
                    {
                        XmlNode root = doc.SelectSingleNode("root");
                        // No tags at all, add a paragraph tag
                        if (root.ChildNodes.Count == 1)
                        {
                            sContent = "<p>" + root.InnerXml + "</p>";
                        }
                    }
                    sContent = HttpUtility.HtmlDecode(sContent);
                }
                catch (Exception ex)
                {
                    Util.AddWarning("Error converting content: " + CopyFrom.Path + ", message: " + ex.Message);
                }
            }
            else if (fromField.Type == "datetime")
            {
                // Convert from xsd null to normal null
                if (fromField.Content == "00010101T000000")
                    sContent = "";
            }

            return sContent;
        }

        private void AddItemToCache(Sitecore6xItem item)
        {
            // Add to cache two times because we might request it by key or by path
            if (!_Options.ExistingTemplates.ContainsKey(item.ID.ToLower()))
                _Options.ExistingTemplates.Add(item.ID.ToLower(), item);
            if (!_Options.ExistingTemplates.ContainsKey(item.Path.ToLower()))
                _Options.ExistingTemplates.Add(item.Path.ToLower(), item);
        }

        public Sitecore6xItem GetTemplateItem()
        {
            Sitecore6xItem tmplateItem = GetSitecore61Item(Util.GuidToSitecoreID(_TemplateID));
            AddItemToCache(tmplateItem);
            return tmplateItem;
        }

        private Sitecore6xItem GetSitecore61Item(string sItemPath)
        {
            return GetSitecore61Item(sItemPath, null);
        }

        private Sitecore6xItem GetSitecore61Item(string sItemPath, Sitecore6xItem parentItem, bool bAllFields = false)
        {
            if (_Options.ExistingTemplates.ContainsKey(sItemPath.ToLower()))
            {
                IItem baseItem = null;
                _Options.ExistingTemplates.TryGetValue(sItemPath.ToLower(), out baseItem);
                return baseItem as Sitecore6xItem;
            }
            XmlNode node = _sitecoreApi.GetXML(sItemPath, false, Util.CurrentDatabase, _credentials);
            XmlNode statusNode = node.SelectSingleNode("status");
            if (statusNode.InnerText == "failed")
                return null;


            return new Sitecore6xItem(node.SelectSingleNode("//item"), parentItem, _sitecoreApi, _credentials, _Options, bAllFields);
        }

        public IItem GetItem(string sItemPath)
        {
            return GetSitecore61Item(sItemPath);
        }


        public static Sitecore6xItem GetItem(string sItemPath, Sitecore61.VisualSitecoreService sitecoreApi, Sitecore61.Credentials credentials, ConverterOptions Options)
        {
            XmlNode node = sitecoreApi.GetXML(sItemPath, false, Util.CurrentDatabase, credentials);
            Sitecore6xItem item = new Sitecore6xItem(node.SelectSingleNode("//item"), null, sitecoreApi, credentials, Options);
            return item;
        }
    }
}
