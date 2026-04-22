using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using EmployManagementSystemAPIs.Connection;
using System.Threading.Tasks;
using System.Collections.Generic;
using EmployeeManagementSystem.Models;
using System.Linq;

namespace EmployManagementSystemAPIs.Services.SalaryInfoServices
{
    class SalaryInfoServices
    {
        public async Task<List<SalaryInfo>> GetAllSalaryInfo()
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var salaryinfo = await connection.QueryAsync<SalaryInfo>(
                    "SELECT * FROM SalaryInfo");
                return salaryinfo.ToList();
            }
        }

        public async Task<SalaryInfo> GetSalaryInfoById(int id)
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var salaryinfo = await connection.QueryAsync<SalaryInfo>(
                    "SELECT * FROM SalaryInfo WHERE Id = @Id",
                    new { Id = id });
                return salaryinfo.FirstOrDefault();
            }
        }

        public async Task<int> PostSalaryInfo(SalaryInfo salaryinfo)
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var result = await connection.ExecuteAsync(
                    "INSERT INTO SalaryInfo (SalaryMonth, BasicSalary, Allowance, Bonus, TotalSalary, BasicId) " +
                    "VALUES (@SalaryMonth, @BasicSalary, @Allowance, @Bonus, @TotalSalary, @BasicId)",
                    new
                    {
                        salaryinfo.SalaryMonth,
                        salaryinfo.BasicSalary,
                        salaryinfo.Allowance,
                        salaryinfo.Bonus,
                        salaryinfo.TotalSalary,
                        salaryinfo.BasicId
                    });
                return result;
            }
        }

        public async Task<int> UpdateSalaryInfo(SalaryInfo salaryinfo)
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var result = await connection.ExecuteAsync(
                    "UPDATE SalaryInfo SET SalaryMonth=@SalaryMonth, BasicSalary=@BasicSalary, " +
                    "Allowance=@Allowance, Bonus=@Bonus, TotalSalary=@TotalSalary, BasicId=@BasicId " +
                    "WHERE Id=@Id",
                    new
                    {
                        salaryinfo.SalaryMonth,
                        salaryinfo.BasicSalary,
                        salaryinfo.Allowance,
                        salaryinfo.Bonus,
                        salaryinfo.TotalSalary,
                        salaryinfo.BasicId,
                        salaryinfo.Id
                    });
                return result;
            }
        }

        public async Task<int> DeleteSalaryInfo(int Id)
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var result = await connection.ExecuteAsync(
                    "DELETE FROM SalaryInfo WHERE Id = @Id",
                    new { Id });
                return result;
            }
        }
    }
}