using EmployeeManagementSystem.Models;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using EmployManagementSystemAPIs.Connection;
using System.Threading.Tasks;

namespace EmployManagementSystemAPIs.Services.BasicInfoServices
{
    class BasicInfoServices
    {
        public async Task<List<BasicInfo>> GetAllBasicInfo()
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var basicinfo = await connection.QueryAsync<BasicInfo>("SELECT * FROM BasicInfo");
                return basicinfo.ToList();
            }
        }

        public async Task<BasicInfo> GetBasicInfoById(int id)
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var basicinfo = await connection.QueryAsync<BasicInfo>(
                    "SELECT * FROM BasicInfo WHERE Id = @Id", new { Id = id });
                return basicinfo.FirstOrDefault();
            }
        }

        public async Task<int> PostBasicInfo(BasicInfo basicinfo)
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var result = await connection.ExecuteAsync(
                    "INSERT INTO BasicInfo (Name, Email, Address, Gender, Position) " +
                    "VALUES (@Name, @Email, @Address, @Gender, @Position)",
                    new
                    {
                        basicinfo.Name,
                        basicinfo.Email,
                        basicinfo.Address,
                        basicinfo.Gender,
                        basicinfo.Position
                    });
                return result;
            }
        }

        public async Task<int> UpdateBasicInfo(BasicInfo basicinfo)
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var result = await connection.ExecuteAsync(
                    "UPDATE BasicInfo SET Name=@Name, Email=@Email, Address=@Address, " +
                    "Gender=@Gender, Position=@Position WHERE Id=@Id",
                    new
                    {
                        basicinfo.Name,
                        basicinfo.Email,
                        basicinfo.Address,
                        basicinfo.Gender,
                        basicinfo.Position,
                        basicinfo.Id
                    });
                return result;
            }
        }

        public async Task<int> DeleteBasicInfo(int Id)
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var result = await connection.ExecuteAsync(
                    "DELETE FROM BasicInfo WHERE Id = @Id",
                    new { Id });
                return result;
            }
        }
    }
}