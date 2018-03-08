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

// © by NAIROLF
// SPS-Mnemonik (E/A - I/Q) in "Adressen / Zuordnungslisten" einfach umschalten
//#############################################################################################
// ChangeLog:
// --------------------------------------------------------------------------------------------
// 2012-12-27   V1.0    NAIROLF Ersterstellung
//#############################################################################################

public class NAIROLF_ContextMenu_ChangePLCMnemonics
{
    public static string sSourceText = string.Empty;           


    [DeclareAction("NAIROLF_ChangePLCMnemonics")]
    public void ChangePLCMnemonik(string DestMnemonik)
    {
        //Prüfe Zielmnemonik
        if (DestMnemonik == string.Empty)
        {
            //keine Zielmnemonik definiert
            return;
        }
        
        //Zwischenablage leeren
        System.Windows.Forms.Clipboard.Clear();

        //Zwischenablage füllen
        CommandLineInterpreter oCLI = new CommandLineInterpreter();
        oCLI.Execute("GfDlgMgrActionIGfWind /function:Copy");

        //Mnemonik-Tausch versuchen
        #region change mnemonik
               
        if (System.Windows.Forms.Clipboard.ContainsText())
        {
            sSourceText = System.Windows.Forms.Clipboard.GetText();
            if (sSourceText != string.Empty)
            {                
                switch (DestMnemonik)
                {
                    case "IQ":
                        //sSourceText = sSourceText.Replace("PED", "PID").Replace("PAD", "PQD").Replace("PEW", "PIW").Replace("PAW", "PQW").Replace("ED", "ID").Replace("AD", "QD").Replace("EW", "IW").Replace("AW", "QW").Replace("EB", "IB").Replace("AB", "QB").Replace("E", "I").Replace("A", "Q");
                        sSourceText = sSourceText.Replace("E", "I").Replace("A", "Q");
                        break;
                    case "EA":
                        //sSourceText = sSourceText.Replace("PID", "PED").Replace("PQD", "PAD").Replace("PIW", "PEW").Replace("PQW", "PAW").Replace("ID", "ED").Replace("QD", "AD").Replace("IW", "EW").Replace("QW", "AW").Replace("IB", "EB").Replace("QB", "AB").Replace("I", "E").Replace("Q", "A");
                        sSourceText = sSourceText.Replace("I", "E").Replace("Q", "A");
                        break;
                    default:
                        return;
                }                

                try
                {
                    System.Windows.Forms.Clipboard.SetText(sSourceText);
                    oCLI.Execute("GfDlgMgrActionIGfWind /function:Paste");
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion
    }

    [DeclareMenu()]
    public void CreateContextMenus()
    {

        Eplan.EplApi.Gui.ContextMenuLocation oCTXLoc = new Eplan.EplApi.Gui.ContextMenuLocation();
        Eplan.EplApi.Gui.ContextMenu oCTXMenu = new Eplan.EplApi.Gui.ContextMenu();

        #region 1st menu-entry
        try
        {           
            oCTXLoc.DialogName = "XPlcIoDataDlg";
            oCTXLoc.ContextMenuName = "1024";
            oCTXMenu.AddMenuItem(oCTXLoc, "[SPS-Mnemonik tauschen]: E/A -> I/Q", "NAIROLF_ChangePLCMnemonics /DestMnemonik:IQ", false, false);
        }
        catch (System.Exception ex)
        {
            MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region 2nd menu-entry
        try
        {            
            oCTXLoc.DialogName = "XPlcIoDataDlg";
            oCTXLoc.ContextMenuName = "1024";
            oCTXMenu.AddMenuItem(oCTXLoc, "[SPS-Mnemonik tauschen]: I/Q -> E/A", "NAIROLF_ChangePLCMnemonics /DestMnemonik:EA", false, false);
        }
        catch (System.Exception ex)
        {
            MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
    }      
}