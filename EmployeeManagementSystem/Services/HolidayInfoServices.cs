using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using EmployManagementSystemAPIs.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;
using System.Linq;

namespace EmployManagementSystemAPIs.Services.HolidayInfoServices
{
    class HolidayInfoServices
    {
        public async Task<List<HolidayInfo>> GetAllHolidayInfo()
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var holidayinfo = await connection.QueryAsync<HolidayInfo>(
                    "SELECT * FROM HolidayInfo");
                return holidayinfo.ToList();
            }
        }

        public async Task<HolidayInfo> GetHolidayInfoById(int id)
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var holidayinfo = await connection.QueryAsync<HolidayInfo>(
                    "SELECT * FROM HolidayInfo WHERE Id = @Id",
                    new { Id = id });
                return holidayinfo.FirstOrDefault();
            }
        }

        public async Task<int> PostHolidayInfo(HolidayInfo holidayinfo)
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var result = await connection.ExecuteAsync(
                    "INSERT INTO HolidayInfo (HolidayMonth, Holidays, Leaves, TotalHolidays, BasicId) " +
                    "VALUES (@HolidayMonth, @Holidays, @Leaves, @TotalHolidays, @BasicId)",
                    new
                    {
                        holidayinfo.HolidayMonth,
                        holidayinfo.Holidays,
                        holidayinfo.Leaves,
                        holidayinfo.TotalHolidays,
                        holidayinfo.BasicId
                    });
                return result;
            }
        }

        public async Task<int> UpdateHolidayInfo(HolidayInfo holidayinfo)
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var result = await connection.ExecuteAsync(
                    "UPDATE HolidayInfo SET HolidayMonth=@HolidayMonth, Holidays=@Holidays, " +
                    "Leaves=@Leaves, TotalHolidays=@TotalHolidays, BasicId=@BasicId " +
                    "WHERE Id=@Id",
                    new
                    {
                        holidayinfo.HolidayMonth,
                        holidayinfo.Holidays,
                        holidayinfo.Leaves,
                        holidayinfo.TotalHolidays,
                        holidayinfo.BasicId,
                        holidayinfo.Id
                    });
                return result;
            }
        }

        public async Task<int> DeleteHolidayInfo(int Id)
        {
            using (IDbConnection connection = new MySqlConnection(DBConnection.dbConnectionString))
            {
                var result = await connection.ExecuteAsync(
                    "DELETE FROM HolidayInfo WHERE Id = @Id",
                    new { Id });
                return result;
            }
        }
    }
}