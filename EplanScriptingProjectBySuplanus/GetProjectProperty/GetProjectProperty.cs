/* Usage
	private static string GetProjectProperty(string id, string index)
	{
		string value = null;
		ActionCallingContext actionCallingContext = new ActionCallingContext();
		actionCallingContext.AddParameter("id", id);
		actionCallingContext.AddParameter("index", index);
		new CommandLineInterpreter().Execute("GetProjectProperty", actionCallingContext);
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


namespace EplanScriptingProjectBySuplanus.GetProjectProperty
{
    class GetProjectProperty
    {
        [DeclareAction("GetProjectProperty")]
        public void Action(string id, string index, out string value)
        {
            string pathTemplate = Path.Combine(PathMap.SubstitutePath("$(MD_SCRIPTS)"),
                "GetProjectProperty", "GetProjectProperty_Template.xml");
            string pathScheme = Path.Combine(PathMap.SubstitutePath("$(MD_SCRIPTS)"),
                "GetProjectProperty", "GetProjectProperty_Scheme.xml");

            try
            {
                // Set scheme
                string content = File.ReadAllText(pathTemplate);
                content = content.Replace("GetProjectProperty_ID", id);
                content = content.Replace("GetProjectProperty_INDEX", index);
                File.WriteAllText(pathScheme, content);

                new Settings().ReadSettings(pathScheme);

                string pathOutput = Path.Combine(
                    PathMap.SubstitutePath("$(MD_SCRIPTS)"), "GetProjectProperty",
                    "GetProjectProperty_Output.txt");

                // Export
                ActionCallingContext actionCallingContext = new ActionCallingContext();
                actionCallingContext.AddParameter("configscheme", "GetProjectProperty");
                actionCallingContext.AddParameter("destinationfile", pathOutput);
                actionCallingContext.AddParameter("language", "de_DE");
                new CommandLineInterpreter().Execute("label", actionCallingContext);

                // Read
                value = File.ReadAllLines(pathOutput)[0];
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "GetProjectProperty", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                value = "[Error]";
            }

        }
    }


}
