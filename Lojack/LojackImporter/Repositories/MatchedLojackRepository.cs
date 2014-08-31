using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Lojack.Data;
using Lojack.Models;
using Lojack.Interfaces;
using RPDSS.Configuration;
using RPDSS.Data;
using System.Configuration;

namespace Lojack.Repositories
{
    public class MatchedLojackRepository : EFRepository<MatchedLojack>, IRepository<MatchedLojack>
    {
        
        public MatchedLojackRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            DataContext = dbContext;
            DbSet = DataContext.Set<MatchedLojack>();
        }
        public int SetLojackMatches()
        {
            using (var db = new LojackContext())
            {
                var sqlResult = db.Database.ExecuteSqlCommand("exec GenerateLojackHits");
                return sqlResult;
            }
        }

        public string GetCentralConnectionString()
        {
            var connectionString = Properties.Settings.Default["RPDSSCentral"].ToString();
            return connectionString;
            Console.WriteLine("ConnectionString: " + connectionString);
        }

        public List<DataMart> GetActiveDataMarts()
        {
            //List<Guid> lJurisdictions = RPDSS.Data.JurisdictionUtility.GetJurisdictionsWithDataMartGuidArray();
            var connectionString = GetCentralConnectionString();
            using (var db = new CentralDb(connectionString))
            {
                var datamarts = db.DataMarts.Where(d => d.IsRemote == false);
                Console.Write("Retrieved data mart list");
                return datamarts.ToList();
            };
        }

        public void GetNewMatches(List<DataMart> dataMarts)
        {
            foreach (var mart in dataMarts)
            {
                using (var db = new LojackContext())
                {
                    Console.WriteLine("Checking matches in " + mart.DatabaseName);
                    var propertyGuids = db.Database.SqlQuery<Guid>(GetSql(mart.DatabaseName)).ToList();
                    Console.WriteLine(propertyGuids.Count.ToString() + " matches found in " + mart.DatabaseName);
                    if (propertyGuids.Any())
                    {
                        List<MatchedLojack> matchedLojacks = GetMatchedRecords(mart, propertyGuids);
                        InsertRecords(db, matchedLojacks);
                    }
                }
            }
        }

        private void InsertRecords(LojackContext db, List<MatchedLojack> matchedLojacks)
        {
            Console.WriteLine("Inserting Records" );
            foreach (var matched in matchedLojacks)
            {
                var newRecord =  Insert(matched);
            }
        }

        public List<MatchedLojack> GetMatchedRecords(DataMart mart, List<Guid> propertyGuid)
        {
            var databaseName = SQLUtility.ExpandName(mart.DatabaseName);
            var db = new LojackContext();
            var matchedRecords = new List<MatchedLojack>();

            using (var connection = new SqlConnection(SQLUtility.GetConnection().ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = GetSqlString(mart.DatabaseName, propertyGuid);
                Console.WriteLine("Opening SQL connection for " + mart.DatabaseName);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("Processing matches for " + mart.DatabaseName);
                    while (reader.Read())
                    {
                        DateTime date;
                        var matchedLojack = new MatchedLojack();
                        matchedLojack.HitGUID = Guid.NewGuid();
                        var o = reader[0];
                        matchedLojack.TransactionGuid = Guid.Parse(reader[0].ToString());
                        matchedLojack.JurisdictionGuid = mart.JurisdictionGuid;
                        matchedLojack.HitCreationDate = DateTime.Now;
                        matchedLojack.PropertyGuid = Guid.Parse(reader[1].ToString());
                        matchedLojack.computracefile = reader[2].ToString();
                        matchedLojack.matchedserial = reader[3].ToString();
                        matchedLojack.custfirstname = reader[4].ToString();
                        matchedLojack.custlastname = reader[5].ToString();
                        matchedLojack.custdob = DateTime.TryParse(reader[6].ToString(), out date) ? date : DateTime.MinValue;
                        matchedLojack.custcity = reader[7].ToString();
                        matchedLojack.custstate = reader[8].ToString();
                        matchedLojack.propertydescription = reader[9].ToString();
                        matchedLojack.transactioneffectivedate = DateTime.TryParse(reader[10].ToString(), out date)
                            ? date
                            : DateTime.MinValue;
                        matchedLojack.discarded = false;
                        matchedLojack.resolved = false;
                        matchedRecords.Add(matchedLojack);
                        Console.Write("Adding matches for " + mart.DatabaseName + "computracefile: " + matchedLojack.computracefile);
                    }

                    return matchedRecords;
                }
            }
        }

        private string GetSqlString(string databaseName, List<Guid>propertyGuid)
        {
            string guid = "";
            for (int i = 0; i < propertyGuid.Count; i++)
            {
                guid = guid + "Cast('" + propertyGuid[i] + "' AS UniqueIdentifier)" + (i < (propertyGuid.Count - 1) ? ", " : "");
            }
            var sql =
                "Select  t.TransactionGUID, p.PropertyGUID, e.computracefile, p.Serial, c.Firstname, c.Lastname, " +
                "c.dateofbirth, a.city, a.statecode, pd.description, t.effectivedate \n" +
                "FROM " + "[" + databaseName + "].dbo.Property p, \n" +
                "[" + databaseName + "].dbo.[Transaction] t, \n" +
                "[" + databaseName + "].dbo.Custdetail c, \n" +
                "[" + databaseName + "].dbo.CustAddress a, \n" +
                "[" + databaseName + "].dbo.PropertyDescription pd, \n" +
                " LOJACK.DBO.LojackEntries e \n" +
                "WHERE \n" +
                "c.custguid = t.custguid \n" +
                "and c.custguid = a.custguid \n" +
                "and p.propertyguid = pd.PropertyGUID  \n" +
                "and t.transactionguid = p.transactionguid  \n" +
                "and e.ReportedSerialNumber = p.Serial \n" +
                "and p.propertyguid in (" + guid + ")" +
                "and e.ReportedSerialNumber not in (SELECT BLACKLISTVALUE FROM " + RPDSS.Data.SQLUtility.ExpandName("BlackList") + " B, " +
                               RPDSS.Data.SQLUtility.ExpandName("BlackListTypeLU") +
                               " L where b.blacklisttypeid = l.blacklisttypeid and l.BLACKLISTTYPE = 'Serial Number')"; 

            return sql;
        }

        //move this to a Datamart repository - when it is created!
        public DataMart FindDataMartByDatabaseName(string databaseName)
        {
            using (var db = new CentralDb())
            {
                var datamart = db.DataMarts.FirstOrDefault(d => d.DatabaseName == databaseName);
                return datamart;
            };
        }
        public string GetSql(string databaseName)
        {
            var central = GetCentralConnectionString();
            var sql = "Select p.PropertyGUID FROM [" + databaseName +
                                                  "].dbo.Property p, Lojack.dbo.LojackEntries e  \n" +
                                                  "Where p.Serial = e.ReportedSerialNumber and \n" +
                                                  "p.PropertyGUID not in (select propertyguid from LOJACK.DBO.MatchedLojack) and \n" +
                                                  "e.ReportedSerialNumber not in (SELECT BLACKLISTVALUE FROM " + RPDSS.Data.SQLUtility.ExpandName("BlackList") + " B, " +
                               RPDSS.Data.SQLUtility.ExpandName("BlackListTypeLU") +
                               " L where b.blacklisttypeid = l.blacklisttypeid and l.BLACKLISTTYPE = 'Serial Number')"; 
                                                  
            Console.WriteLine("SQL: " + sql);
            return sql;
        }

        #region GetLojackHits SP - to be converted to code at some point?
        //var jurisdictions = JurisdictionUtility.GetJurisdictions();

        //        SET @insertstring = '
        //INSERT INTO LOJACK.dbo.MatchedLojack
        //    (Hitguid,
        //     TRansactionGuid,
        //     JurisdictionGuid, 
        //     HitCreationDate, 
        //     PropertyGuid,
        //     computracefile,
        //     matchedserial, 
        //     discarded, 
        //     resolved, 
        //     custfirstname, 
        //     custlastname,
        //     custdob,
        //     custcity, 
        //     custstate,
        //     propertydescription,
        //     transactioneffectivedate)
        //     VALUES ( SELECT NEWID(), 
        //        t.TransactionGUID, 
        //        '''+ cast(@jurisdiction as varchar(40)) + ''' as JurisdictionGuid, 
        //        GETDATE(), 
        //        p.PropertyGUID, 
        //        computracefile, 
        //        p.Serial, 
        //        0, 
        //        0,
        //        c.Firstname, 
        //        c.Lastname, 
        //        c.dateofbirth,
        //        a.city,
        //        a.statecode, 
        //        pd.description,
        //        t.effectivedate
        //        FROM
        //         [{dbname}].dbo.Property p, 
        //         [{dbname}].dbo.[Transaction] t,
        //         [{dbname}].dbo.Custdetail c, 
        //         [{dbname}].dbo.CustAddress a, 
        //         [{dbname}].dbo.Propertydescription pd, 
        //         LOJACK.DBO.LojackEntries e 
        //        WHERE
        //         c.custguid = t.custguid
        //          and c.custguid = a.custguid
        //          and p.propertyguid = pd.propertyguid 
        //          and t.transactionguid = p.transactionguid 
        //          and p.Serial = e.ReportedSerialNumber 
        //          and p.PropertyGUID not in (select propertyguid from LOJACK.DBO.MatchedLojack))'

        //SET @usedb = REPLACE(@insertstring, '{dbname}',@dbname)
        #endregion

    }
}
