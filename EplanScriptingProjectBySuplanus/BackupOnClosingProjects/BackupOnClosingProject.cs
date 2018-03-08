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


public class BackupOnClosingProject
{
	[DeclareEventHandler("onActionStart.String.XPrjActionProjectClose")]
	public void Function()
	{
		string strProjectname = PathMap.SubstitutePath("$(PROJECTNAME)");
		string strFullProjectname = PathMap.SubstitutePath("$(P)");
		string strDestination = strFullProjectname;
		
		DialogResult Result = MessageBox.Show(
			"Soll eine Sicherung für das Projekt\n'"
			+ strProjectname +
			"'\nerzeugt werden?",
			"Datensicherung",
			MessageBoxButtons.YesNo,
			MessageBoxIcon.Question
			);

		if (Result == DialogResult.Yes)

		  {

				string myTime = System.DateTime.Now.ToString("yyyy.MM.dd");
				string hour = System.DateTime.Now.Hour.ToString();
				string minute = System.DateTime.Now.Minute.ToString();

				Progress progress = new Progress("SimpleProgress");
				progress.BeginPart(100, "");
				progress.SetAllowCancel(true);

				if (!progress.Canceled())
				{
					progress.BeginPart(33, "Backup");
					ActionCallingContext backupContext = new ActionCallingContext();
					backupContext.AddParameter("BACKUPMEDIA", "DISK");
					backupContext.AddParameter("BACKUPMETHOD", "BACKUP");
					backupContext.AddParameter("COMPRESSPRJ", "0");
					backupContext.AddParameter("INCLEXTDOCS", "1");
					backupContext.AddParameter("BACKUPAMOUNT", "BACKUPAMOUNT_ALL");
					backupContext.AddParameter("INCLIMAGES", "1");
					backupContext.AddParameter("LogMsgActionDone", "true");
					backupContext.AddParameter("DESTINATIONPATH", strDestination);
					backupContext.AddParameter("PROJECTNAME", strFullProjectname);
					backupContext.AddParameter("TYPE", "PROJECT");
					backupContext.AddParameter("ARCHIVENAME", strProjectname + "_" + myTime + "_" + hour + "." + minute + ".");
					new CommandLineInterpreter().Execute("backup", backupContext);
					progress.EndPart();
					
				}
				progress.EndPart(true);
			}
			
			return;
		}
	}