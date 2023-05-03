// See https://aka.ms/new-console-template for more information


using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;

using static BTM.Rep0;

//Console.WriteLine("Hello, World!");
using System.Security.AccessControl;

//Console.WriteLine("Hello, World!");
using  BTM.Tree;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace BTM
{
    class Program
    {
        static void Main(string[] args)
        {
            #region rep0
            Rep0.Line L016, L023, L014;
            Rep0.Stop S01, S02, S03, S04, S05, S06, S07, S08, S09;

            L016 = new Rep0.Line(10.ToString(), 16, "SIMD");
            L023 = new Rep0.Line(17.ToString(), 23, "Isengard - Mordor");
            L014 = new Rep0.Line("E", 14, "Museum of Plant");

            S01 = new Rep0.Stop(1, "SPIR - V", "bus", new List<Rep0.Line> { L016 });
            S02 = new Rep0.Stop(2, "GLSL", "tram", new List<Rep0.Line> { L016 });
            S03 = new Rep0.Stop(3, "HLSL", "other", new List<Rep0.Line> { L016 });
            S04 = new Rep0.Stop(4, "Dol Guldur", "bus", new List<Rep0.Line> { L023 });
            S05 = new Rep0.Stop(5, "Amon Hen", "bus", new List<Rep0.Line> { L023 });
            S06 = new Rep0.Stop(6, "Gondolin", "bus", new List<Rep0.Line> { L023 });
            S07 = new Rep0.Stop(7, "Bitazon", "tram", new List<Rep0.Line> { L023, L014 });
            S08 = new Rep0.Stop(8, "Bytecroft", "bus", new List<Rep0.Line> { L016, L023 });
            S09 = new Rep0.Stop(9, "Maple", "other", new List<Rep0.Line> { L014 });

            L016.setStops = new List<Rep0.Stop> { S01, S02, S03, S08 };
            L023.setStops = new List<Rep0.Stop> { S04, S05, S06, S07 };
            L014.setStops = new List<Rep0.Stop> { S07, S08, S09 };

            Rep0.Bytebus B011 = new Rep0.Bytebus(11, null, "Byte5");
            Rep0.Bytebus B012 = new Rep0.Bytebus(12, null, "bisel20");
            Rep0.Bytebus B013 = new Rep0.Bytebus(13, null, "bisel20");
            Rep0.Bytebus B014 = new Rep0.Bytebus(14, null, "gibgaz");
            Rep0.Bytebus B015 = new Rep0.Bytebus(15, null, "gibgaz");

            B011.setLines = new List<Rep0.Line> { L016, L023 };
            B012.setLines = new List<Rep0.Line> { L016 };
            B013.setLines = new List<Rep0.Line> { L016 };
            B014.setLines = new List<Rep0.Line> { L023, L014 };
            B015.setLines = new List<Rep0.Line> { L023 };

            Rep0.Tram T021 = new Rep0.Tram(21, 1, L014);
            Rep0.Tram T022 = new Rep0.Tram(22, 2, L014);
            Rep0.Tram T023 = new Rep0.Tram(23, 6, L014);

            Rep0.Driver D01 = new Rep0.Driver("Tomas", "Chairman", 20, new List<Rep0.Vehicle> { B011, T021, B015 });
            Rep0.Driver D02 = new Rep0.Driver("Tomas", "Thetank", 4, new List<Rep0.Vehicle> { B012, B013, B014 });
            Rep0.Driver D03 = new Rep0.Driver("Oru", "Bii", 55, new List<Rep0.Vehicle> { T022, T023 });

            List<Rep0.Line> lines0 = new List<Rep0.Line> { L023, L016, L014 };
            List<Rep0.Stop> stops0 = new List<Rep0.Stop> { S01, S02, S03, S04, S05, S06, S07, S08, S09 };
            List<Rep0.Vehicle> vehicles0 = new List<Rep0.Vehicle> { B011, B012, B013, B014, B015, T021, T022, T023 };
            List<Rep0.Driver> drivers0 = new List<Rep0.Driver> { D01, D02, D03 };
            List<Rep0.Bytebus> bytebuses0 = new List<Rep0.Bytebus> { B011, B012, B013, B014,B015 };
            List<Rep0.Tram> trams0 = new List<Rep0.Tram> { T021,T022,T023 };


            L016.setVehicles = new List<Rep0.Vehicle> { B011, B012, B013 };
            L023.setVehicles = new List<Rep0.Vehicle> { B011, B014, B015 };
            L014.setVehicles = new List<Rep0.Vehicle> { B014, T021, T022, T023 };

            foreach (Rep0.Driver d in drivers0)
            {
                foreach (var v in d.GetAdaptVehicles)
                {
                    v.AddDriver(d);
                }
            }

            #endregion rep0

            #region rep6
            ////////////////////////
            // reprezenatcja 6//
            ////////////////////////////
            ///

            Adapt_rep6_Line A6L_16, A6L_23, A6L_14;
            Adapt_rep6_Stop A6S_1, A6S_2, A6S_3, A6S_4, A6S_5, A6S_6, A6S_7, A6S_8, A6S_9;

            A6L_16 = new(10.ToString(), 16, "SIMD");
            A6L_23 = new(17.ToString(), 23, "Isengard - Mordor");
            A6L_14 = new("E", 14, "Museum of Plant");

            A6S_1 = new(1, "SPIR - V", "bus", new List<Adapt_rep6_Line> { A6L_16 });
            A6S_2 = new(2, "GLSL", "tram", new List<Adapt_rep6_Line> { A6L_16 });
            A6S_3 = new(3, "HLSL", "other", new List<Adapt_rep6_Line> { A6L_16 });
            A6S_4 = new(4, "Dol Guldur", "bus", new List<Adapt_rep6_Line> { A6L_23 });
            A6S_5 = new(5, "Amon Hen", "bus", new List<Adapt_rep6_Line> { A6L_23 });
            A6S_6 = new(6, "Gondolin", "bus", new List<Adapt_rep6_Line> { A6L_23 });
            A6S_7 = new(7, "Bitazon", "tram", new List<Adapt_rep6_Line> { A6L_23, A6L_14 });
            A6S_8 = new(8, "Bytecroft", "bus", new List<Adapt_rep6_Line> { A6L_16, A6L_23 });
            A6S_9 = new(9, "Maple", "other", new List<Adapt_rep6_Line> { A6L_14 });

            A6L_16.SetStops = new List<Adapt_rep6_Stop> { A6S_1, A6S_2, A6S_3, A6S_8 };
            A6L_23.SetStops = new List<Adapt_rep6_Stop> { A6S_4, A6S_5, A6S_6, A6S_7 };
            A6L_14.SetStops = new List<Adapt_rep6_Stop> { A6S_7, A6S_8, A6S_9 };

            Adapt_rep6_Bytebus A6B_11 = new(11, new List<Adapt_rep6_Line> { A6L_16, A6L_23 }, "Byte5");
            Adapt_rep6_Bytebus A6B_12 = new(12, new List<Adapt_rep6_Line> { A6L_16 }, "bisel20");
            Adapt_rep6_Bytebus A6B_13 = new(13, new List<Adapt_rep6_Line> { A6L_16 }, "bisel20");
            Adapt_rep6_Bytebus A6B_14 = new(14, new List<Adapt_rep6_Line> { A6L_23, A6L_14 }, "gibgaz");
            Adapt_rep6_Bytebus A6B_15 = new(15, new List<Adapt_rep6_Line> { A6L_23 }, "gibgaz");

            Adapt_rep6_Tram A6T_21 = new(21, 1, A6L_14);
            Adapt_rep6_Tram A6T_22 = new(22, 2, A6L_14);
            Adapt_rep6_Tram A6T_23 = new(23, 6, A6L_14);

            Adapt_rep6_Driver A6D_1 = new("Tomas", "Chairman", 20, new List<Adapt_rep6_Vehicle> { A6B_11, A6T_21, A6B_15 });
            Adapt_rep6_Driver A6D_2 = new("Tomas", "Thetank", 4, new List<Adapt_rep6_Vehicle> { A6B_12, A6B_13, A6B_14 });
            Adapt_rep6_Driver A6D_3 = new("Oru", "Bii", 55, new List<Adapt_rep6_Vehicle> { A6T_22, A6T_23 });

            List<Adapt_rep6_Line> lines6 = new List<Adapt_rep6_Line> { A6L_16, A6L_23, A6L_14 };
            List<Adapt_rep6_Stop> stops6 = new List<Adapt_rep6_Stop> { A6S_1, A6S_2, A6S_3, A6S_4, A6S_5, A6S_6, A6S_7, A6S_8, A6S_9 };
            List<Adapt_rep6_Vehicle> vehicles6 = new List<Adapt_rep6_Vehicle> { A6B_11, A6B_12, A6B_13, A6B_14, A6B_15, A6T_21, A6T_22, A6T_23 };
            List<Adapt_rep6_Driver> drivers6 = new List<Adapt_rep6_Driver> { A6D_1, A6D_2, A6D_3 };

            A6L_16.setVehicles = new List<Adapt_rep6_Vehicle> { A6B_11, A6B_12, A6B_13 };
            A6L_23.setVehicles = new List<Adapt_rep6_Vehicle> { A6B_11, A6B_14, A6B_15 };
            A6L_14.setVehicles = new List<Adapt_rep6_Vehicle> { A6B_14, A6T_21, A6T_22, A6T_23 };

            foreach (Adapt_rep6_Driver d in drivers6)
            {
                foreach (var v in d.GetAdaptVehicles)
                {
                    v.AddDriver(d);
                }
            }
            #endregion rep6

            #region rep4
            Adapt_rep4_Line A4L_16, A4L_23, A4L_14;
            Adapt_rep4_Stop A4S_1, A4S_2, A4S_3, A4S_4, A4S_5, A4S_6, A4S_7, A4S_8, A4S_9;

            A4L_16 = new(10.ToString(), 16, "SIMD");
            A4L_23 = new(17.ToString(), 23, "Isengard - Mordor");
            A4L_14 = new("E", 14, "Museum of Plant");

            A4S_1 = new(1, "SPIR - V", "bus", new List<Adapt_rep4_Line> { A4L_16 });
            A4S_2 = new(2, "GLSL", "tram", new List<Adapt_rep4_Line> { A4L_16 });
            A4S_3 = new(3, "HLSL", "other", new List<Adapt_rep4_Line> { A4L_16 });
            A4S_4 = new(4, "Dol Guldur", "bus", new List<Adapt_rep4_Line> { A4L_23 });
            A4S_5 = new(5, "Amon Hen", "bus", new List<Adapt_rep4_Line> { A4L_23 });
            A4S_6 = new(6, "Gondolin", "bus", new List<Adapt_rep4_Line> { A4L_23 });
            A4S_7 = new(7, "Bitazon", "tram", new List<Adapt_rep4_Line> { A4L_23, A4L_14 });
            A4S_8 = new(8, "Bytecroft", "bus", new List<Adapt_rep4_Line> { A4L_16, A4L_23 });
            A4S_9 = new(9, "Maple", "other", new List<Adapt_rep4_Line> { A4L_14 });

            A4L_16.SetStops = new List<Adapt_rep4_Stop> { A4S_1, A4S_2, A4S_3, A4S_8 };
            A4L_23.SetStops = new List<Adapt_rep4_Stop> { A4S_4, A4S_5, A4S_6, A4S_7 };
            A4L_14.SetStops = new List<Adapt_rep4_Stop> { A4S_7, A4S_8, A4S_9 };

            Adapt_rep4_Bytebus A4B_11 = new(11, new List<Adapt_rep4_Line> { A4L_16, A4L_23 }, "Byte5");
            Adapt_rep4_Bytebus A4B_12 = new(12, new List<Adapt_rep4_Line> { A4L_16 }, "bisel20");
            Adapt_rep4_Bytebus A4B_13 = new(13, new List<Adapt_rep4_Line> { A4L_16 }, "bisel20");
            Adapt_rep4_Bytebus A4B_14 = new(14, new List<Adapt_rep4_Line> { A4L_23, A4L_14 }, "gibgaz");
            Adapt_rep4_Bytebus A4B_15 = new(15, new List<Adapt_rep4_Line> { A4L_23 }, "gibgaz");

            Adapt_rep4_Tram A4T_21 = new(21, 1, A4L_14);
            Adapt_rep4_Tram A4T_22 = new(22, 2, A4L_14);
            Adapt_rep4_Tram A4T_23 = new(23, 6, A4L_14);

            Adapt_rep4_Driver A4D_1 = new("Tomas", "Chairman", 20, new List<Adapt_rep4_Vehicle> { A4B_11, A4T_21, A4B_15 });
            Adapt_rep4_Driver A4D_2 = new("Tomas", "Thetank", 4, new List<Adapt_rep4_Vehicle> { A4B_12, A4B_13, A4B_14 });
            Adapt_rep4_Driver A4D_3 = new("Oru", "Bii", 55, new List<Adapt_rep4_Vehicle> { A4T_22, A4T_23 });

            List<Adapt_rep4_Line> lines4 = new List<Adapt_rep4_Line> { A4L_16, A4L_23, A4L_14 };
            List<Adapt_rep4_Stop> stops4 = new List<Adapt_rep4_Stop> { A4S_1, A4S_2, A4S_3, A4S_4, A4S_5, A4S_6, A4S_7, A4S_8, A4S_9 };
            List<Adapt_rep4_Vehicle> vehicles4 = new List<Adapt_rep4_Vehicle> { A4B_11, A4B_12, A4B_13, A4B_14, A4B_15, A4T_21, A4T_22, A4T_23 };
            List<Adapt_rep4_Driver> drivers4 = new List<Adapt_rep4_Driver> { A4D_1, A4D_2, A4D_3 };

            A4L_16.SetVehicles = new List<Adapt_rep4_Vehicle> { A4B_11, A4B_12, A4B_13 };
            A4L_23.SetVehicles = new List<Adapt_rep4_Vehicle> { A4B_11, A4B_14, A4B_15 };
            A4L_14.SetVehicles = new List<Adapt_rep4_Vehicle> { A4B_14, A4T_21, A4T_22, A4T_23 };

            foreach (Adapt_rep4_Driver d in drivers4)
            {
                foreach (var v in d.GetAdaptVehicles)
                {
                    v.AddDriver(d);
                }
            }

            BinaryTree<ILine> lines = new BinaryTree<ILine>(new List<ILine>(lines0));
            BinaryTree<IVehicle> vehicles = new BinaryTree<IVehicle>(new List<IVehicle>(vehicles0));
            BinaryTree<IStop> stops = new BinaryTree<IStop>(new List<IStop>(stops0));
            BinaryTree<IDriver> drivers = new BinaryTree<IDriver>(new List<IDriver>(drivers0));
            BinaryTree<IBytebus> bytebuses = new BinaryTree<IBytebus>(new List<IBytebus>(bytebuses0));
            BinaryTree<ITram> trams = new BinaryTree<ITram>(new List<ITram>(trams0));

            Func<ILine, bool> pred = Algorithms.f;
            BiList<ILine> biLines0 = new BiList<ILine>(new List<ILine>(lines0));
            BinaryTree<ILine> binaryTreeLines0 = new BinaryTree<ILine>(new List<ILine>(lines0));
            Vector<ILine> vectorLines = new Vector<ILine>(new List<ILine>(lines0));

            DataStorer dataStorer = DataStorer.createDataStorer(lines, bytebuses, trams, drivers, stops);
            #endregion rep4

            #region createDataforCommands
            string[] possibleTypes = new string[] { "lines", "stops", "bytebus", "trams", "drivers" };
            List<String> possibleType = new List<String>(possibleTypes);
            Dictionary<string, ICommand> commandMap = new Dictionary<string, ICommand>();
            CommandList commandList = new CommandList(dataStorer);
            CommandFind commandFind = new CommandFind(dataStorer);
            CommandAdd commandAdd = new CommandAdd(dataStorer);
            commandMap.Add("list",commandList);
            commandMap.Add("find", commandFind);
            commandMap.Add("add", commandAdd);
            
            //commandMap.Add("add",)
            ICommand? command = null;
            #endregion createDataforCommands


            while (true)
            {
                string? commandLine = Console.ReadLine();
                if (commandLine == null) continue;
                string commandLIne = commandLine.ToLower();
                string[] words = System.Text.RegularExpressions.Regex.Split(commandLine, "\"([^\"]*)\"|(\\s+)");
                List<string> wordsList = new List<string>(words);
                wordsList.RemoveAll(item => item ==" " || item =="");
                words = wordsList.ToArray();
                string commandName = words[0];
                if (commandName == "exit") break;

                if (commandMap.ContainsKey(commandName) == false)
                {
                    Console.WriteLine("Invalid Command");
                    continue;
                }
                command = commandMap[commandName];

                command.execute(commandLine.ToLower());
            }
            Console.WriteLine("Terminating...");
            return;
        }
    }
}