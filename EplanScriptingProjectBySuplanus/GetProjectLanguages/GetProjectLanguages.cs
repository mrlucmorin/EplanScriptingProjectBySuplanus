﻿/* Usage
    private static string GetProjectLanguages()
    {
        string value = null;
        ActionCallingContext actionCallingContext = new ActionCallingContext();
        new CommandLineInterpreter().Execute("GetProjectLanguages", actionCallingContext);
        actionCallingContext.GetParameter("value", ref value);
        return value;
    }
*/

/*
The following compiler directive is necessary to enable editing scripts
within Visual Studio.

It requires that the "Conditional compilation symbol" SCRIPTENV be defined 
in the Visual Studio project properties

This is because EPLAN's internal scripting engine already adds "using directives"
when you load the script in EPLAN. Having them twice would cause errors.
*/

#if SCRIPTENV
using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Scripting;
using Eplan.EplApi.Base;
using Eplan.EplApi.Gui;
#endif

/*
On the other hand, some namespaces are not automatically added by EPLAN when
you load a script. Those have to be outside of the previous conditional compiler directive
*/

using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace EplanScriptingProjectBySuplanus.GetProjectLanguages
{
    public class GetProjectLanguages
    {
        private readonly string TempPath = Path.Combine(
            PathMap.SubstitutePath("$(TMP)"), "GetProjectLanguages.xml");

        [DeclareAction("GetProjectLanguages")]
        public void Action(out string value)
        {
            ActionCallingContext actionCallingContext = new ActionCallingContext();
            actionCallingContext.AddParameter("prj", FullProjectPath());
            actionCallingContext.AddParameter("node", "TRANSLATEGUI");
            actionCallingContext.AddParameter("XMLFile", TempPath);
            new CommandLineInterpreter().Execute("XSettingsExport", actionCallingContext);

            if (File.Exists(TempPath))
            {
                string languagesString = GetValueSettingsXml(TempPath,
                    "/Settings/CAT/MOD/Setting[@name='TRANSLATE_LANGUAGES']/Val");

                if (languagesString != null)
                {
                    List<string> languages = languagesString.Split(';').ToList();
                    languages = languages.Where(obj => !obj.Equals("")).ToList();

                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < languages.Count; i++)
                    {
                        var language = languages[i];
                        stringBuilder.Append(language);

                        // not last one
                        if (i != languages.Count - 1)
                        {
                            stringBuilder.Append("|");
                        }
                    }

                    // returns list: "de_DE|en_EN"
                    value = stringBuilder.ToString();
                    return;
                }
            }

            value = null;
            return;
        }

        // Returns the EPLAN Project Path
        private static string FullProjectPath()
        {
            ActionCallingContext acc = new ActionCallingContext();
            acc.AddParameter("TYPE", "PROJECT");

            string projectPath = string.Empty;
            new CommandLineInterpreter().Execute("selectionset", acc);
            acc.GetParameter("PROJECT", ref projectPath);

            return projectPath;
        }

        // Read EPLAN XML-ProjectInfo and returns the value
        private static string GetValueSettingsXml(string filename, string url)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filename);

            XmlNodeList rankListSchemaName = xmlDocument.SelectNodes(url);
            if (rankListSchemaName != null && rankListSchemaName.Count > 0)
            {
                // Get Text from MultiLanguage or not :)
                string value = rankListSchemaName[0].InnerText;
                return value;
            }
            else
            {
                return null;
            }
        }
    }
}