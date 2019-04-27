using iBank.Core.BankProvider;

using System;
using System.Collections.Generic;

namespace iBank.BankProvider.VTB
{
    public static class BankProviderPersonExtensions
    {
        public static IEnumerable<BankProviderPerson> GetExecutedByDate(DateTime date)
        {
            var sql = $@"
SELECT *
FROM [Анкеты]
WHERE [Анкеты].[Исключить] = False AND [Анкеты].[Исполнено] = True AND [Анкеты].[Серия ДУЛ] <> '' AND [Анкеты].[Номер ДУЛ] <> '' AND DateValue([Анкеты].[ДатаИсполнения]) = DateValue('{date}')
ORDER BY [Анкеты].[ДатаИсполнения];";
            using (var cmd = new OdbcCommandExecutor(sql))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    yield return new BankProviderPerson(reader);
            }
        }
        public static IEnumerable<BankProviderPerson> GetAll()
        {
            var sql = $@"
SELECT *
FROM Анкеты
WHERE [Анкеты].[Исключить] = False AND [Анкеты].[Исполнено] = True AND [Анкеты].[Серия ДУЛ] <> '' AND [Анкеты].[Номер ДУЛ] <> ''
ORDER BY [Анкеты].[ДатаИсполнения];";
            using (var cmd = new OdbcCommandExecutor(sql))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    yield return new BankProviderPerson(reader);
            }
        }
    }
}