﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMSConverter.Core
{
    public interface IItem
    {
        string ID { get; }
        string Name { get; }
        string Key { get; }
        string Path { get; }
        string Icon { get; set; }
        string SortOrder { get; set; }

        IItem[] Templates { get; }
        IItem BaseTemplate { get; }
        IField[] Fields { get; }
        IRole[] Roles { get; }
        IRole[] Users { get; }

        IItem Parent { get; }
        IItem[] GetChildren();
        IItem GetItem(string sItemPath);

        void CopyTo(IItem CopyFrom, bool bRecursive);
        bool MoveTo(IItem MoveTo);
        void Delete();
        void Save();
        string AddFromTemplate(string sName, string sTemplatePath);
        bool HasChildren();
        string[] GetLanguages();

        ConverterOptions Options { get; }

        // Helper functions
        string GetOuterXml();
        string GetHostUrl();
    }
}
