﻿using Microsoft.EntityFrameworkCore;

namespace FiscalLabApp.Repositories;

public class DatabaseService<T> where T : DbContext
{
    public static string FileName = "/database/app.db";
    private readonly IDbContextFactory<T> _dbContextFactory;
#if RELEASE
        
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
#endif


#if DEBUG
    public DatabaseService()
    {
    }
#else
        public DatabaseService(IJSRuntime jsRuntime
            , IDbContextFactory<T> dbContextFactory)
        {
            if (jsRuntime == null) throw new ArgumentNullException(nameof(jsRuntime));
            _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));

            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./js/file.js").AsTask());
        }
#endif

    public async Task InitDatabaseAsync()
    {
        try
        {
#if RELEASE
                var module = await _moduleTask.Value;
                await module.InvokeVoidAsync("mountAndInitializeDb");
                if (!File.Exists(FileName))
                {
                    File.Create(FileName).Close();
                }

                await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
                await dbContext.Database.EnsureCreatedAsync();
#endif
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.GetType().Name, ex.Message);
        }
    }
}