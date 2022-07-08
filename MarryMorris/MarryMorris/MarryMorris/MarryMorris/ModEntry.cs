using System;
using System.Collections.Generic;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace MarryMorris
{
    public class ModEntry: Mod
    {
        public Dictionary<int, SchedulePathDescription> getMorrisSchedule()
        {
            int dayOfMonth = Game1.dayOfMonth;
            NPC Morris = Game1.getCharacterFromName("MorrisTod");
            Dictionary<string, string> dictionary;

            try
            {
                dictionary = Game1.content.Load<Dictionary<string, string>>("Characters\\schedules\\" + Morris.name);
            }
            catch (Exception ex)
            {
                this.Monitor.Log("Could not load Morris's schedule.", LogLevel.Error);
                return (Dictionary<int, SchedulePathDescription>)null;
            }

            if (Morris.isMarried())
            {
                string str = Game1.shortDayNameFromDayOfSeason(dayOfMonth);
                if ((str.Equals("Mon") || str.Equals("Tue") || str.Equals("Wed") || str.Equals("Thu") || str.Equals("Fri")) && (Game1.MasterPlayer.mailReceived.Contains("ccIsComplete") || Game1.MasterPlayer.hasCompletedCommunityCenter()))
                {
                    this.Monitor.Log("Loading Morris's regular marriage work schedule.", LogLevel.Debug);
                    return Morris.parseMasterSchedule(dictionary["marriageJob"]);
                }
                else if ( Game1.MasterPlayer.hasCompletedCommunityCenter() == false)
                {
                    this.Monitor.Log("Loading Morris's regular marriage work schedule.", LogLevel.Debug);
                    return Morris.parseMasterSchedule(dictionary["marriageJob"]);
                }

                else
                {
                    this.Monitor.Log("Loading Morris's regular schedule.", LogLevel.Debug);
                    return Morris.getSchedule(dayOfMonth);
                }
            }

            else
            {
                this.Monitor.Log("Loading Morris's regular schedule.", LogLevel.Debug);
                return Morris.getSchedule(dayOfMonth);
            }
        }
        public Dictionary<int, SchedulePathDescription> getMorrisWednesdaySchedule()
        {
            int dayOfMonth = Game1.dayOfMonth;
            NPC Morris = Game1.getCharacterFromName("MorrisTod");
            Dictionary<string, string> dictionary;

            try
            {
                dictionary = Game1.content.Load<Dictionary<string, string>>("Characters\\schedules\\" + Morris.name);
            }
            catch (Exception ex)
            {
                this.Monitor.Log("Could not load Morris's schedule.", LogLevel.Error);
                return (Dictionary<int, SchedulePathDescription>)null;
            }

            if (Morris.isMarried())
            {
                string str = Game1.shortDayNameFromDayOfSeason(dayOfMonth);

                if (str.Equals("Wed"))
                {
                    this.Monitor.Log("Loading Morris's Wednesday marriage work schedule.", LogLevel.Debug);
                    return Morris.parseMasterSchedule(dictionary["marriageJobWed"]);
                }

                else
                {
                    this.Monitor.Log("Loading Morris's regular marriage work schedule.", LogLevel.Debug);
                    return Morris.parseMasterSchedule(dictionary["marriageJob"]);
                }
            }

            else
            {
                this.Monitor.Log("Loading Morris's regular schedule.", LogLevel.Debug);
                return Morris.getSchedule(dayOfMonth);
            }
        }

        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.DayStarted += this.OnDayStarted;
        }

        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            //Game1.getCharacterFromName("MorrisTod").Schedule = getMorrisSchedule();

            bool isLoaded = this.Helper.ModRegistry.IsLoaded("FlashShifter.SVECode");

            //Adds Morris back into the game after SVE removes him

            if (isLoaded && (Game1.MasterPlayer.mailReceived.Contains("ccIsComplete") || Game1.MasterPlayer.hasCompletedCommunityCenter()))
            {
                
                Game1.getCharacterFromName("MorrisTod").currentLocation.addCharacter(Game1.getCharacterFromName("MorrisTod"));
                this.Monitor.Log("Adding Morris back to " + Game1.getCharacterFromName("MorrisTod").currentLocation.Name, LogLevel.Debug);
              
            }

            //Gets Morris's schedule when married
            
            /*if (Game1.player.spouse == "MorrisTod" && (((Game1.dayOfMonth + 4) % 7) == 0) && (Game1.MasterPlayer.mailReceived.Contains("ccIsComplete") || Game1.MasterPlayer.hasCompletedCommunityCenter()))
            {
                Game1.getCharacterFromName("MorrisTod").Schedule = getMorrisWednesdaySchedule();
            }
            else if (Game1.player.spouse == "MorrisTod")
            {
                Game1.getCharacterFromName("MorrisTod").Schedule = getMorrisSchedule();
            }*/
            
        }

    }
}
