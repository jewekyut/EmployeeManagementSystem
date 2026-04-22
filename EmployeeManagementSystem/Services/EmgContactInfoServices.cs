using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using EmployManagementSystemAPIs.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;
using System.Linq;

namespace EmployManagementSystemAPIs.Services.EmgContactInfoServices
{
    class EmgContactInfoServices
    {
        public async Task<List<EmgContactInfo>> GetAllEmgContactInfo()
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var emgcontactinfo = await connection.QueryAsync<EmgContactInfo>(
                    "SELECT * FROM EmgContactInfo");
                return emgcontactinfo.ToList();
            }
        }

        public async Task<EmgContactInfo> GetEmgContactInfoById(int id)
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var emgcontactinfo = await connection.QueryAsync<EmgContactInfo>(
                    "SELECT * FROM EmgContactInfo WHERE Id = @Id",
                    new { Id = id });
                return emgcontactinfo.FirstOrDefault();
            }
        }

        public async Task<int> PostEmgContactInfo(EmgContactInfo emgcontactinfo)
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var result = await connection.ExecuteAsync(
                    "INSERT INTO EmgContactInfo (BasicId, EmgContactName, EmgContactPhone, EmgContactEmail) " +
                    "VALUES (@BasicId, @EmgContactName, @EmgContactPhone, @EmgContactEmail)",
                    new
                    {
                        emgcontactinfo.BasicId,
                        emgcontactinfo.EmgContactName,
                        emgcontactinfo.EmgContactPhone,
                        emgcontactinfo.EmgContactEmail
                    });
                return result;
            }
        }

        public async Task<int> UpdateEmgContactInfo(EmgContactInfo emgcontactinfo)
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var result = await connection.ExecuteAsync(
                    "UPDATE EmgContactInfo SET BasicId=@BasicId, EmgContactName=@EmgContactName, " +
                    "EmgContactPhone=@EmgContactPhone, EmgContactEmail=@EmgContactEmail WHERE Id=@Id",
                    new
                    {
                        emgcontactinfo.BasicId,
                        emgcontactinfo.EmgContactName,
                        emgcontactinfo.EmgContactPhone,
                        emgcontactinfo.EmgContactEmail,
                        emgcontactinfo.Id
                    });
                return result;
            }
        }

        public async Task<int> DeleteEmgContactInfo(int Id)
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var result = await connection.ExecuteAsync(
                    "DELETE FROM EmgContactInfo WHERE Id = @Id",
                    new { Id });
                return result;
            }
        }
    }
}