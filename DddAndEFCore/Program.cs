
public static void Main(string[] args)
{

    string conexString = GetConnectionString();
    ILoggerFactory loggerFactory = CreateLoggerFactory();

    var optionBuilder = new DbContextOptionsBuilder<SchoolContext>();
    
    optionBuilder.UseSqlServer(conexString)
                 .UseLoggerFactory(loggerFactory)
                 .EnableSensitiveDataLogging(true);

    using (var context = new SchoolContext(optionBuilder.Options))
    {
        Student student = context.Students.Find(1L);
        student.Name += " _Updated";
        student.Email = "_Updated";

        context.SaveChanges();
    }

    private static ILoggerFactory CreateLoggerFactory()
        {
            return LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                    .AddConsole();
            });
        }

    private static string GetConnectionString()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            return configuration["ConnectionString"];

        
    }


}