using Pelorus.Core.Synchronization;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pelorus.Core.Test.Integration
{
    [TestClass]
    public class MutualExclusionTests
    {
        [TestMethod]
        public void TestDistributedMutualExclusions()
        {
            using (var trans = new TransactionScope())
            {
                var connection = new SqlConnection("Server=.;Database=Sandbox;Trusted_Connection=True;");
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO [dbo].[tblLock] ([Name]) VALUES ('Test0');";
                    connection.Open();
                    cmd.ExecuteNonQuery();

                    using (new DistributedMutualExclusion("LOCK0", 30))
                    {
                        Console.WriteLine("Inside the lock block.");

                        using (new DistributedMutualExclusion("LOCK0"))
                        {
                            Console.WriteLine("Inside the nested lock block.");
                        }

                        using (new DistributedMutualExclusion("LOCK1"))
                        {
                            Console.WriteLine("Inside the nested lock block with a different lock name.");
                        }

                        Thread.Sleep(5000);
                    }
                }

                trans.Complete();
            }
        }

        [TestMethod]
        public void TestParallelLocks()
        {
            Action loopBody0 = () =>
            {
                Console.WriteLine("0: Entering block.");

                using (new DistributedMutualExclusion("LOCK0", 10))
                {
                    Console.WriteLine("0: Inside the lock block.");

                    using (new DistributedMutualExclusion("LOCK0", 10))
                    {
                        Console.WriteLine("0: Inside the nested lock block.");
                    }

                    using (new DistributedMutualExclusion("LOCK1", 10))
                    {
                        Console.WriteLine("0: Inside the nested lock block with a different lock name.");
                    }

                    Console.WriteLine("0: Waiting in block.");
                    Thread.Sleep(5000);
                    Console.WriteLine("0: Leaving block.");
                }
            };

            Action loopBody1 = () =>
            {
                Console.WriteLine("1: Entering block.");
                Console.WriteLine("1: Waiting in block.");
                Thread.Sleep(200);
                Console.WriteLine("1: Done waiting.");

                using (new DistributedMutualExclusion("LOCK0", 10))
                {
                    Console.WriteLine("1: Inside the lock block.");

                    using (new DistributedMutualExclusion("LOCK0", 10))
                    {
                        Console.WriteLine("1: Inside the nested lock block.");
                    }

                    using (new DistributedMutualExclusion("LOCK1", 10))
                    {
                        Console.WriteLine("1: Inside the nested lock block with a different lock name.");
                    }

                    Console.WriteLine("1: Waiting in block.");
                    Thread.Sleep(5000);
                    Console.WriteLine("1: Leaving block.");
                }
            };

            Parallel.Invoke(loopBody0, loopBody1);
        }
    }
}
