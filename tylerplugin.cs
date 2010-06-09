///----------------------------------------------------------------------------
///
/// File Name: tylerplugin.cs
/// This file was written for Tyler Kushera to add disclaimers
/// to AIM conversations
///
///----------------------------------------------------------------------------

using System;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;
using AccCoreLib;
using System.Diagnostics;


namespace tylerAIM
{
// dev key	[GuidAttribute("77693134-6172-646C-7572-575F4357626D")]
    [GuidAttribute("7769316C-5255-3965-4761-4A79445F7649")]
	public class tylerplugin : IAccPlugin, IAccCommandTarget
	{
		#region Plugin Registration
		[ComRegisterFunctionAttribute]
		public static void RegisterFunction(Type t)
		{
			RegistryKey key = Registry.LocalMachine.CreateSubKey(PluginKeyName(t));
			key.SetValue("Name", t.Name);
            key.SetValue("Version", "1.0");
            key.SetValue("Description", "Allows you to configure an automatic disclaimer message");
            key.SetValue("Url", "http://www.wied.us/software.html");
		} 

		[ComUnregisterFunctionAttribute]
		public static void UnregisterFunction(Type t)
		{
			Registry.LocalMachine.DeleteSubKey(PluginKeyName(t));
		} 
		private static string PluginKeyName(Type t)
		{
			return "Software\\America Online\\AIM\\Plugins\\" + 
				'{' + t.GUID.ToString() + '}';
		} 
        #endregion

		#region IAccPlugin Members

		public void Init(AccSession session, IAccPluginInfo pluginInfo)
		{
            TextWriterTraceListener tr = new TextWriterTraceListener("d:\\trace.log");
            Trace.Listeners.Add(tr);
            Trace.AutoFlush = true;

            m_session = session;

			// this will force a callback function / delegate call when IMs are sent
			m_session.BeforeImSend += new DAccEvents_BeforeImSendEventHandler(m_session_sendhandler);

            m_lastSent = DateTime.Now.AddMinutes(-20);
            m_reminderInterval = 1;


            Trace.WriteLine("trying to read prefs");

            // read these values from the preferences

            IAccPreferences thePrefs = m_session.Prefs;

            try
            {     
                m_selectedBuddyGroup = (String) thePrefs.GetValue("aimcc.Disclaimer.UseGroup");
                m_disclaimerText = (String)thePrefs.GetValue("aimcc.Disclaimer.Text");
                m_reminderInterval = (int)thePrefs.GetValue("aimcc.Disclaimer.TimeInterval");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
            Trace.WriteLine("group: " + m_selectedBuddyGroup);
            Trace.WriteLine("text: " + m_disclaimerText);

            Trace.WriteLine("made it past prefs");
		}

    	// runs before the IM is sent

		private void m_session_sendhandler(AccSession piSession,
										   IAccImSession theIMSession,
										   IAccParticipant theParticipant,
										   IAccIm theIM)
		{
            // determine if we're sending to someone in that group                

                // get the "hot" group
                IAccBuddyList theBuddyList = piSession.BuddyList;
                IAccGroup dcGroup = theBuddyList.GetGroupByName(m_selectedBuddyGroup);

                bool foundThem = false;

                for (int i = 0; i < dcGroup.BuddyCount; i++)
                {
                    // iterate through the whole group and see if
                    // the person we are talking to is on the list
                    IAccUser dcUser = dcGroup.GetBuddyByIndex(i);

                    // create a session for the group user since AIM sucks and this is the
                    // easiest way to decide of we are conversing
                    // with this person

                    AccImSessionType theSessType =
                            (AccImSessionType) theIMSession.get_Property(AccImSessionProp.AccImSessionProp_SessionType);

                    IAccImSession dummySession =
                            piSession.CreateImSession(dcUser.Name,
                                                      theSessType);
                    // since AIM will return a the same session if we
                    // are already talking with them, compare them
                    // if they are equal we have the person

                    if (dummySession == theIMSession)
                    {
                        foundThem = true;
                        break;
                    }
                }
                if (foundThem)
                {
                    TimeSpan theSpan = DateTime.Now.Subtract(m_lastSent);
                    Trace.WriteLine("session handler");
                    Trace.WriteLine("timespan is " + theSpan.Minutes);
                    Trace.WriteLine("interval is " + m_reminderInterval);
                    if (theSpan.Minutes > m_reminderInterval)
                    {
                        // ok we found them
                        theIM.Text = m_disclaimerText + " " + theIM.Text;
                        m_lastSent = DateTime.Now;
                    }                   
                }
             
		}

		public void Shutdown()
		{
			// TODO: Add Class1.Shutdown implementation
			m_session = null;
		}   
         
		public void Exec(int command, object users)
		{
		    if (command == (int)AccCommandId.AccCommandId_Preferences)
			{
                // iterate through all the groups and add their names
                // to the arraylist so we have a combo box value

                Trace.WriteLine("running set preferences dialog");
                
                IAccBuddyList theList = m_session.BuddyList;
                ArrayList stuff = new ArrayList();
 
                for (int i = 0; i < theList.GroupCount; i++)
                {
                    stuff.Add(theList.GetGroupByIndex(i).Name);
                }

                Trace.WriteLine("populated the choice array");
                try
                {
                    m_thePluginForm = new PluginConfig(stuff,
                                                       m_disclaimerText,
                                                       m_selectedBuddyGroup,
                                                       m_reminderInterval);

                    Trace.WriteLine("constructed form");
                    m_thePluginForm.FormClosed += new FormClosedEventHandler(pluginForm_FormClosed);
                    Trace.WriteLine("show form");
                    m_thePluginForm.Show();
                    Trace.WriteLine("form shown");
                }
                catch (Exception theE)
                {
                    Trace.WriteLine(theE);
                }
			}
		}

        void pluginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // persist the stuff from the plugin config now that
            // the form has closed

            if (m_thePluginForm.formSaved)
            {
                m_selectedBuddyGroup = m_thePluginForm.theSelectedBuddy;
                m_disclaimerText = m_thePluginForm.theDisclaimerMessage;
                m_reminderInterval = m_thePluginForm.theTimeInterval;
            }

            Trace.WriteLine("saw form closed event in main plugin");
            Trace.WriteLine("group: " + m_selectedBuddyGroup);
            Trace.WriteLine("text: " + m_disclaimerText);

            IAccPreferences thePrefs = m_session.Prefs;
            try
            {
                thePrefs.SetValue("aimcc.Disclaimer.UseGroup", (object)m_selectedBuddyGroup);
                thePrefs.SetValue("aimcc.Disclaimer.Text", (object)m_disclaimerText);
                thePrefs.SetValue("aimcc.Disclaimer.TimeInterval", (object) m_reminderInterval);
            }
            catch (Exception exc)
            {
                Trace.WriteLine(exc);
            }
        }


		public bool QueryStatus(int command, object users)
		{
			return true;
		} 

		#endregion

		#region IACCPlugin Attributes

		private AccSession m_session;
        private String m_disclaimerText;
        private String m_selectedBuddyGroup;
        private int m_reminderInterval;
        private PluginConfig m_thePluginForm;
        private DateTime m_lastSent;

		#endregion


	} // of class
} // of namespace

