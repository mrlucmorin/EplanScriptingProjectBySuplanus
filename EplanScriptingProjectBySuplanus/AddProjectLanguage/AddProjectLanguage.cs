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

internal class AddProjectLanguageClass
{
    [Start]
    public void XAfActionSettingProject_Start()
    {
        CommandLineInterpreter oCLI = new CommandLineInterpreter();
        ActionCallingContext oACC = new ActionCallingContext();
        oACC.AddParameter("set", "TRANSLATEGUI.TRANSLATE_LANGUAGES");
        oACC.AddParameter("value", "de_DE;en_EN;zh_CN;");

        oCLI.Execute("XAfActionSettingProject", oACC);
        return;
    }
}

