using System;
using System.Collections.Generic;
using System.Linq;
using Respawn;
using Xunit;

namespace EfIdentities
{
    public class Tests
    {
        public Tests()
        {
            var checkpoint = new Checkpoint();
            checkpoint.TablesToIgnore = new[] {"__MigrationHistory"};
            checkpoint.Reset(OrdersDbContext.ConnectionString);
        }

        [Fact]
        public void CanSaveAnOrderWithOneLine()
        {
            var order = new Order
            {
                Created = DateTime.Now,
                Lines = new List<OrderLine>
                {
                    new OrderLine
                    {
                        Product = "something"
                    }
                }
            };

            using (var db = new OrdersDbContext())
            {
                db.Orders.Add(order);
                db.SaveChanges();
            }

            using (var db = new OrdersDbContext())
            {
                Assert.Equal(1, db.Orders.Count());
                var dbOrder = db.Orders.First();

                Assert.Equal(1, dbOrder.Lines.Count);
                Assert.Equal("something", dbOrder.Lines.First().Product);
            }
        }

        [Fact]
        public void CanSaveOrderWithTwoLines()
        {
            CreateOrderWithTwoLines();

            using (var db = new OrdersDbContext())
            {
                var dbOrder = db.Orders.First();
                
                Assert.Equal(2, dbOrder.Lines.Count);
            }
        }

        [Fact]
        public void DeletingOrderWillDeleteLines()
        {
            CreateOrderWithTwoLines();

            using (var db = new OrdersDbContext())
            {
                var dbOrder = db.Orders.First();

                db.Orders.Remove(dbOrder);
                db.SaveChanges();
            }

            using (var db = new OrdersDbContext())
            {
                Assert.Equal(0, db.Orders.Count());
                Assert.Equal(0, db.OrderLines.Count());
            }
        }

        [Fact]
        public void DeletingLineWillNotLeaveOrphans()
        {
            CreateOrderWithTwoLines();

            using (var db = new OrdersDbContext())
            {
                var dbOrder = db.Orders.First();

                dbOrder.Lines.Remove(dbOrder.Lines.ElementAt(0));
                db.SaveChanges();
            }

            using (var db = new OrdersDbContext())
            {
                Assert.Equal(1, db.Orders.Count());
                Assert.Equal(1, db.OrderLines.Count());
            }
        }

        private static void CreateOrderWithTwoLines()
        {
            var order = new Order
            {
                Created = DateTime.Now,
                Lines = new List<OrderLine>
                {
                    new OrderLine
                    {
                        Product = "something"
                    },
                    new OrderLine
                    {
                        Product = "anything"
                    }
                }
            };

            using (var db = new OrdersDbContext())
            {
                db.Orders.Add(order);
                db.SaveChanges();
            }
        }
    }
}