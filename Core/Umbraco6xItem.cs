using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMSConverter.Core.Umbraco6DocumentService;
using System.Xml;

namespace CMSConverter.Core
{
    public class Umbraco6xAPI
    {
        public Umbraco6DocumentService.documentService _umbracoDocumentApi = null;
        public Umbraco6xWebService.webService _umbracoWebService = null;
        public Credentials _credentials = null;

        public Umbraco6xAPI(string sUrl, Credentials credentials)
        {
            _umbracoDocumentApi = new Umbraco6DocumentService.documentService();
            _umbracoDocumentApi.Url = sUrl + "/umbraco/webServices/api/DocumentService.asmx";
//            _umbracoDocumentApi.PreAuthenticate = true;

            _umbracoWebService = new Umbraco6xWebService.webService();
            _umbracoWebService.Url = sUrl + "/umbraco/webServices/api/DocumentService.asmx";


            _credentials = credentials;
        }
    }

    public class Umbraco6xItem : IItem
    {
        private Umbraco6xAPI _umbracoAPI = null;
        private ConverterOptions _Options = null; 
        private string _sName = "";

        #region IItem Members

        public static Umbraco6xItem GetRoot(Umbraco6xAPI umbracoAPI, ConverterOptions Options)
        {
            return new Umbraco6xItem(null, umbracoAPI, Options);
        }

        private Umbraco6xItem(Umbraco6xItem parent, Umbraco6xAPI umbracoAPI, ConverterOptions Options)
        {
            _umbracoAPI = umbracoAPI;
            _Options = Options;

//            XmlNode node = umbracoAPI._umbracoWebService.GetDocumentValidate(0, umbracoAPI._credentials.UserName, umbracoAPI._credentials.Password);
//            node = umbracoAPI._umbracoWebService.GetDocument(0, "");
            int iID = 0;
            if (parent != null)
                iID = int.Parse(parent.ID.ToString());

            documentCarrier[] docCarrierList = _umbracoAPI._umbracoDocumentApi.readList(iID, umbracoAPI._credentials.UserName, umbracoAPI._credentials.Password);
            documentCarrier docCarrier = _umbracoAPI._umbracoDocumentApi.read(1105, umbracoAPI._credentials.UserName, umbracoAPI._credentials.Password);
            _sName = docCarrier.Name;
        }

        public string ID
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public string Key
        {
            get { throw new NotImplementedException(); }
        }

        public string Path
        {
            get { throw new NotImplementedException(); }
        }

        public string Icon
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string SortOrder
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IItem[] Templates
        {
            get { throw new NotImplementedException(); }
        }

        public IItem BaseTemplate
        {
            get { throw new NotImplementedException(); }
        }

        public IField[] Fields
        {
            get { throw new NotImplementedException(); }
        }

        public IRole[] Roles
        {
            get { throw new NotImplementedException(); }
        }

        public IRole[] Users
        {
            get { throw new NotImplementedException(); }
        }

        public IItem Parent
        {
            get { throw new NotImplementedException(); }
        }

        public IItem[] GetChildren()
        {
            throw new NotImplementedException();
        }

        public IItem GetItem(string sItemPath)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IItem CopyFrom, bool bRecursive)
        {
            throw new NotImplementedException();
        }

        public bool MoveTo(IItem MoveTo)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public string AddFromTemplate(string sName, string sTemplatePath)
        {
            throw new NotImplementedException();
        }

        public bool HasChildren()
        {
            throw new NotImplementedException();
        }

        public string[] GetLanguages()
        {
            throw new NotImplementedException();
        }

        public ConverterOptions Options
        {
            get
            {
                return _Options;
            }
            set
            {
                _Options = value;
            }
        }

        public string GetOuterXml()
        {
            throw new NotImplementedException();
        }

        public string GetHostUrl()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
