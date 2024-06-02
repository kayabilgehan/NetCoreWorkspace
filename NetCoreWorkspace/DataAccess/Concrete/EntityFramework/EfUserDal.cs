using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework {
	public class EfUserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal {
		public List<OperationClaim> GetClaims(User user) {
			using (var context = new NorthwindContext()) {
				var result = from operationClaim in context.OperationClaims
							 join userOperationClaim in context.UserOperationClaims
								 on operationClaim.Id equals userOperationClaim.OperationClaimId
							 where userOperationClaim.UserId == user.Id
							 select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
				return result.ToList();
			}
		}

		public void InsertCustomData<T>(Dictionary<string, List<T>> tableModels) {
			if (tableModels == null || tableModels.Count == 0) {
				throw new ArgumentException("Table models dictionary cannot be null or empty.");
			}
			// (p.Name != "ID" && p.Name != "Id" && p.Name != "id")
			using (var context = new NorthwindContext()) 
			{
				var allParameters = new List<SqlParameter>();
				var allSqlCommands = new List<string>();
				var paramIndex = 0; // Start parameter index from the total count

				foreach (var kvp in tableModels) {
					var tableName = kvp.Key;
					var models = kvp.Value;

					if (models == null || models.Count == 0) {
						continue; // Skip if the list of models is empty
					}

					// Get the properties of the model excluding "ID" column
					var properties = typeof(T).GetProperties().Where(p => p.CanWrite && p.GetMethod.IsPublic && (p.Name != "ID" && p.Name != "Id" && p.Name != "id"));
					var columns = string.Join(", ", properties.Select(p => $"[{p.Name}]"));

					var sql = new StringBuilder($"INSERT INTO {tableName} ({columns}) VALUES ");
					var parameters = new List<SqlParameter>();

					foreach (var model in models) {
						var valuePlaceholders = new List<string>();

						foreach (var prop in properties) {
							var paramName = $"@{prop.Name}_{paramIndex}";
							var propValue = prop.GetValue(model) ?? DBNull.Value;

							valuePlaceholders.Add(paramName);
							parameters.Add(new SqlParameter(paramName, propValue));
							paramIndex++;
						}

						sql.Append($"({string.Join(", ", valuePlaceholders)}),");
					}

					// Remove the last comma
					sql.Length--;

					allSqlCommands.Add(sql.ToString());
					allParameters.AddRange(parameters);
				}

				// Combine all SQL commands into a single command
				var combinedSql = string.Join("; ", allSqlCommands);

				// Execute the combined SQL command once
				context.Database.ExecuteSqlRaw(combinedSql, allParameters.ToArray());
			}
		}
	}
}
