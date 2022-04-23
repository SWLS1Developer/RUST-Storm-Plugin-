using System;
using Oxide.Core;

namespace Oxide.Plugins
{
    [Info("Storm", "Developer", 1.0)]
    [Description("Storm plugin")]
    public class StormPlugin : RustPlugin
    {

        Timer t1;
        Timer t2;
        Timer t3;
        Timer t4;
        private void Init()
        {
         //   permission.RegisterPermission("StormPlugin.use", this);
        }
        [ConsoleCommand("rndstorm")]
        void RandomStorm(ConsoleSystem.Arg arg)
        {
            
        }

            [ConsoleCommand("startstorm")]
        void StartStorm(ConsoleSystem.Arg arg) 
        {
            // rain %100, fog %79, env.oceanlevel:0 --> max_ocean_level
            if (arg.Args.Length != 0)
            {
                if (arg.IsServerside)
                {
                    int max_ocean_level = Convert.ToInt32(arg.Args[0]);
                    int second_between_ocean_level_rise = Convert.ToInt32(arg.Args[1]);
                    int secs_to_Wait_when_storm_reaches_max = Convert.ToInt32(arg.Args[3]) * 60;
                    bool announce = Convert.ToBoolean(arg.Args[2]);

    

                    //Server.Command("weather.rain 100");                // rain = 100%
                    //Server.Command("weather.fog 79");                 //fog = 79%
                    //Server.Command("weather.clouds 99");
                    //Server.Command("weather.wind 90");

                    Server.Command("weather.load storm");                // rain = 100% 
                 //   Server.Command("weather.load Fog");                 //fog = 79%

                    t3 = timer.Once(25f, () =>
                           {

                               if (announce == true)
                               {
                                   PrintToChat("<color=red> UYARI: </color><color=yellow> FIRTINA GELIYOR!</color>");
                               }


     


                               int i_ = 0;

                               t1 = timer.Repeat(second_between_ocean_level_rise, max_ocean_level + 1, () =>
                               {
                                   if (max_ocean_level <= i_)
                                   {

                                     if (secs_to_Wait_when_storm_reaches_max != 0)
                                       {
                                           t4 = timer.Once(secs_to_Wait_when_storm_reaches_max, () =>
                                           {
                                               int _i = max_ocean_level;
                                               if (announce == true)
                                               {
                                                   PrintToChat("<color=red> UYARI: </color><color=yellow> FIRTINA DURUYOR!</color>");
                                               }

                                               t2 = timer.Repeat(1f, max_ocean_level + 1, () =>
                                               { 
                                                   if (_i <= 0)
                                                   {
                                                       Server.Command("weather.reset");
                                                       //Server.Command("weather.fog 0");
                                                       //Server.Command("weather.clouds 0");
                                                       //Server.Command("weather.wind 0");

                                                       if (announce == true)
                                                       {
                                                           PrintToChat("<color=green> UYARI: </color><color=yellow> FIRTINA DURDU</color>");
                                                       }
                                                   }
                                                   else
                                                   {
                                                       _i = _i - 1;
                                                       Server.Command("env.oceanlevel " + _i);
                                                   }
                                               });
                                           });
                                       }  else
                                       {
                                           int _i = max_ocean_level;
                                           if (announce == true)
                                           {
                                               PrintToChat("<color=red> UYARI: </color><color=yellow> FIRTINA DURUYOR!</color>");
                                           }

                                           t2 = timer.Repeat(3f, max_ocean_level + 1, () =>
                                           {
                                               if (_i <= 0)
                                               {
                                                   Server.Command("weather.rain 0");
                                                   Server.Command("weather.fog 0");
                                                   if (announce == true)
                                                   {
                                                       PrintToChat("<color=green> UYARI: </color><color=yellow> FIRTINA DURDU</color>");
                                                   }
                                               }
                                               else
                                               {
                                                   _i = _i - 1;
                                                   Server.Command("env.oceanlevel " + _i);
                                               }
                                           });
                                       }

                                   }
                                   else
                                   {
                                       i_ = i_ + 1;
                                       Server.Command("env.oceanlevel " + i_);
                              //        PrintToConsole("Ocean level: " + i_);

                                   }
                               });

                           });


                }        
               
            } else  {PrintToChat("Right usage: startstorm <end_level> <secs_between_levels (+2 levels per time)> <announce true/false> <minutes to wait when level is at max>");
        }
        }

        [ConsoleCommand("stopstorm")]
        void StopStorm(ConsoleSystem.Arg arg)
        {
            // rain %0, fog %0, env.oceanlevel:0
            //    Server.Command("env.oceanlevel 0");
                        Server.Command("weather.reset");                           
            //Server.Command("weather.rain 0");
            //Server.Command("weather.fog 0");
            //Server.Command("weather.clouds 0");
            //Server.Command("weather.wind 0");
            t1.Destroy();
             t2.Destroy(); 

        }
    }
}
