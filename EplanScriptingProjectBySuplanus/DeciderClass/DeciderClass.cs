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

class DeciderClass
{
    [Start]
    public void Function()
    {
        #region Decider
        Decider decider = new Decider();
        EnumDecisionReturn decision = decider.Decide(
            EnumDecisionType.eOkCancelDecision, // type
            "This is the text",
            "Title",
            EnumDecisionReturn.eOK, // selected Answer
            EnumDecisionReturn.eOK); // Answer if quite-mode on

        switch (decision)
        {
            case EnumDecisionReturn.eOK:
                // OK
                break;

            case EnumDecisionReturn.eCANCEL:
                // Cancel
                break;
        } 
        #endregion


        #region FileSelector
        FileSelectDecisionContext fileContext = new FileSelectDecisionContext("MySelector", EnumDecisionReturn.eCANCEL);
        fileContext.Title = "Title";
        fileContext.CustomDefaultPath = @"C:\MyDefaultPath";
        fileContext.OpenFileFlag = true; // true=Open, false=save
        fileContext.AllowMultiSelect = true;
        fileContext.DefaultExtension = "xml";
        fileContext.AddFilter("XML files (*.xml)", "*.xml");
        fileContext.AddFilter("All files (*.*)", "*.*");

        Decider oDecision = new Decider();
        EnumDecisionReturn eAnswer = oDecision.Decide(fileContext);
        if (eAnswer != EnumDecisionReturn.eOK)
        {
            foreach (string file in fileContext.GetFiles())
            {
                // do with file
            }
        } 
        #endregion

    }
}

