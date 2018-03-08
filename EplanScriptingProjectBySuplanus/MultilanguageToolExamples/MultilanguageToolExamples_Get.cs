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


public class MultilanguageToolExamples_Get
{
    [Start]
    public void MultilanguageToolExamples_Get_Void()
    {
        CommandLineInterpreter oCLI = new CommandLineInterpreter();
        ActionCallingContext acc = new ActionCallingContext();
        string ActionReturnParameterValue = string.Empty;
        string strMessage = string.Empty;

        #region GetProjectLanguages
        oCLI.Execute("GetProjectLanguages", acc);
        acc.GetParameter("LANGUAGELIST", ref ActionReturnParameterValue);
        string[] ProjectLanguages = ActionReturnParameterValue.Split(';');

        foreach (string s in ProjectLanguages)
        {
            strMessage = strMessage + s + "\n";
        }
        MessageBox.Show(strMessage, "GetProjectLanguages");
        strMessage = string.Empty;
        #endregion

        #region GetDisplayLanguages
        oCLI.Execute("GetDisplayLanguages", acc);
        acc.GetParameter("LANGUAGELIST", ref ActionReturnParameterValue);
        string[] DisplayLanguages = ActionReturnParameterValue.Split(';');

        foreach(string s in DisplayLanguages)
        {
            strMessage = strMessage + s + "\n";
        }
        MessageBox.Show(strMessage, "GetDisplayLanguages");
        strMessage = string.Empty;
        #endregion

        #region GetVariableLanguage
        oCLI.Execute("GetVariableLanguage", acc);
        acc.GetParameter("LANGUAGELIST", ref ActionReturnParameterValue);
        string VariableLanguage = ActionReturnParameterValue;
        strMessage = strMessage + VariableLanguage + "\n";
        MessageBox.Show(strMessage, "GetVariableLanguage");
        strMessage = string.Empty;
        #endregion

    }
}