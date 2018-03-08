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


public class SelectLanguage_UsingExample
{
    [Start]
    public void Execute()
    {
        CommandLineInterpreter oCLI = new CommandLineInterpreter();
        ActionCallingContext acc = new ActionCallingContext();
        string ActionReturnParameterValue = string.Empty;

        acc.AddParameter("Language", "Project"); // parameters: "Project" or "Display"
        acc.AddParameter("DialogName", "MyDialogName");
        oCLI.Execute("SelectLanguage", acc);
        acc.GetParameter("Language", ref ActionReturnParameterValue);

        MessageBox.Show("Language from SelectLanguage:\n\n---> " + ActionReturnParameterValue);

    }
}
