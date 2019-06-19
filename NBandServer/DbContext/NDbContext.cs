using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NBandServer
{
    public class NDbContext : DbContext
    {
        public DbSet<FileMission> FileMissionTable { get; set; }


        ///代码实例
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            /// 除了startup里面通过appsetting配置之外，还可以直接在此配置 如下：
            /// server = 123.207.31.37; port = 3306; database = efTest; uid = root; password = Mei19931129; sslmode = none
            string conn = Module.mysqlConnstr;
            optionsBuilder.UseMySQL(conn);//配置连接字符串 必须加sslmode=none
        }
        public int Update<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] updatedProperties) where TEntity : class
        {
            var dbEntityEntry = this.Entry(entity);
            if (updatedProperties.Any())
            {
                foreach (var property in updatedProperties)
                {
                    dbEntityEntry.Property(property).IsModified = true;
                }
            }
            else
            {
                foreach (var property in dbEntityEntry.OriginalValues.Properties)
                {
                    string pName = property.Name;
                    var original = dbEntityEntry.OriginalValues.GetValue<object>(pName);
                    var current = dbEntityEntry.CurrentValues.GetValue<object>(pName);
                    if (original != null && !original.Equals(current))
                    {
                        dbEntityEntry.Property(pName).IsModified = true;
                    }
                }
            }
            return this.SaveChanges();
        }
    }
}
