using NinjaDomain.Classes;
using NinjaDomain.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            _ = args;


            Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>());


            InsertNinja();

            WriteSeparator();

            InsertMultipleNinjas();

            WriteSeparator();

            SimpleNinjaQueries();

            WriteSeparator();

            QueryAndUpdateNinja();

            WriteSeparator();

            QueryAndUpdateNinjaDisconnected();

            WriteSeparator();

            RetrieveDataWithFind();

            WriteSeparator();

            RetrieveDataWithStoredProc();

            WriteSeparator();

            DeleteNinja();

            WriteSeparator();

            DeleteNinjaWithKeyValue();

            WriteSeparator();

            DeleteNinjaViaStoredProcedure();

            WriteSeparator();

            InsertNinjaWithEquipment();

            WriteSeparator();

            SimpleNinjaGraphQuery();

            WriteSeparator();

            ProjectionQuery();


            Console.ReadKey();
        }

        private static void InsertNinja()
        {
            var ninja = new Ninja
            {
                Name = "SampsonSan",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(2008, 1, 28),
                ClanId = 1
            };

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.Add(ninja);
                context.SaveChanges();
            }
        }

        private static void WriteSeparator()
        {
            Console.WriteLine("---");
            Console.ReadKey();
        }

        private static void InsertMultipleNinjas()
        {
            var ninja1 = new Ninja
            {
                Name = "Leonardo",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1984, 1, 1),
                ClanId = 1
            };

            var ninja2 = new Ninja
            {
                Name = "Raphael",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1985, 1, 1),
                ClanId = 1
            };

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.AddRange(new List<Ninja> { ninja1, ninja2 });
                context.SaveChanges();
            }
        }

        private static void SimpleNinjaQueries()
        {
            using (var context = new NinjaContext())
            {
                var ninja = context.Ninjas
                            .Where(n => n.DateOfBirth >= new DateTime(1984, 1, 1))
                            .OrderBy(n => n.Name)
                            .Skip(1).Take(1)
                            .FirstOrDefault();
                //var query = context.Ninjas;
                //var someninjas = query.ToList();

                //foreach (var ninja in ninjas)
                //{
                Console.WriteLine(ninja.Name);
                //}
            }
        }

        private static void QueryAndUpdateNinja()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.FirstOrDefault();
                ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);
                context.SaveChanges();
            }
        }

        private static void QueryAndUpdateNinjaDisconnected()
        {
            Ninja ninja;

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                ninja = context.Ninjas.FirstOrDefault();
            }

            ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.Attach(ninja);
                context.Entry(ninja).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        private static void RetrieveDataWithFind()
        {
            var keyval = 4;

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.Find(keyval);
                Console.WriteLine("After Find#1:" + ninja.Name);

                var someNinja = context.Ninjas.Find(keyval);
                Console.WriteLine("After Find#2:" + someNinja.Name);
                ninja = null;
            }
        }

        private static void RetrieveDataWithStoredProc()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninjas = context.Ninjas.SqlQuery("exec GetOldNinjas").ToList();
                //foreach (var ninja in ninjas)
                //{
                //    Console.WriteLine(ninja.Name);
                //}
            }
        }

        private static void DeleteNinja()
        {
            Ninja ninja;

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                ninja = context.Ninjas.FirstOrDefault();
                //context.Ninjas.Remove(ninja);
                //context.SaveChanges();
            }

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                //context.Ninjas.Attach(ninja);
                //context.Ninjas.Remove(ninja);
                context.Entry(ninja).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Niepotrzebne pobranie obiektu po id - żeby usunąć wystarczy id więc po co piebierać cały obiekt skoro id jest już znane.
        /// Lepiej ustawić Entry State na Deleted lub wykorzystać Attach i Delete.
        /// </summary>
        private static void DeleteNinjaWithKeyValue()
        {
            var keyval = 1;

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.Find(keyval);
                context.Ninjas.Remove(ninja);
                context.SaveChanges();
            }
        }

        private static void DeleteNinjaViaStoredProcedure()
        {
            var keyval = 3;

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Database.ExecuteSqlCommand("exec DeleteNinjaViaId {0}", keyval);
            }
        }

        private static void InsertNinjaWithEquipment()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var ninja = new Ninja
                {
                    Name = "Kacy Catanzaro",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1990, 1, 14),
                    ClanId = 1
                };

                var muscles = new NinjaEquipment
                {
                    Name = "Muscles",
                    Type = EquipmentType.Tool
                };

                var spunk = new NinjaEquipment
                {
                    Name = "Spunk",
                    Type = EquipmentType.Weapon
                };

                context.Ninjas.Add(ninja);
                ninja.EquipmentOwned.Add(muscles);
                ninja.EquipmentOwned.Add(spunk);
                context.SaveChanges();
            }
        }

        private static void SimpleNinjaGraphQuery()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                //var ninjas = context.Ninjas.Include(n => n.EquipmentOwned)
                //    .FirstOrDefault(n => n.Name.StartsWith("Kacy"));
                var ninja = context.Ninjas
                    .FirstOrDefault(n => n.Name.StartsWith("Kacy"));
                Console.WriteLine("Ninja Retrieved:" + ninja.Name);
                //context.Entry(ninja).Collection(n => n.EquipmentOwned).Load();
                Console.WriteLine("Ninja Equipment Count: {0}", ninja.EquipmentOwned.Count());
            }
        }

        private static void ProjectionQuery()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninjas = context.Ninjas
                    .Select(n => new { n.Name, n.DateOfBirth, n.EquipmentOwned })
                    .ToList();
            }
        }
    }
}
