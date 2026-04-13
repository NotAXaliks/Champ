1. Устанавливаешь dotnet ef

`dotnet tool install --global dotnet-ef`

Добавляешь Design для скаффолда бд
`dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.14`

Устанавливаем пакет
`Microsoft.EntityFrameworkCore.Templates`

`dotnet new ef-templates`

В файле EntityType.t4
1. Находим `var usings` в котором "System", Строка 30
Добавляем  "System.Text.Json.Serialization", "Microsoft.AspNetCore.Mvc.ModelBinding", "Microsoft.AspNetCore.Mvc.ModelBinding.Validation"

2. Находим `ICollection`, в нее [ValidateNever, BindNever]
3. снизу тоже, строка 135

    public virtual ...

    Добавляем вверху `[ValidateNever, BindNever]`

После создания БД
`dotnet ef dbcontext scaffold "Server=127.0.0.1:40001;Username=xaliks;Password=coolPaSsw0rd;Database=champ" "Npgsql.EntityFrameworkCore.PostgreSQL" -o "Models" --no-build --force`

"Jwt": {
    "Key": "fksjdhfkjdshfkjdshfkjdshfjkdshfkjsdhfkjdshfjkdshfjkdshfjksdhfjkhds",
    "Issuer": "dsfgsdfgdfsgdsfgfds",
    "Audience": "dsfgsdfgdfsgdsfgfds"
}

в App.xaml.cs Добавляешь
Dispatcher.UIThread.UnhandledException += OnUnhandledException;

private void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
{
    e.Handled = true;
}


Переходишь в /home/USER/.nuget/Packages/microsoft.visualstudio....mvc/../Templates
Копируешь ControllerGenerator в созданный Templates/. Меняешь всё там

`dotnet aspnet-codegenerator controller -name ProductsController2 -m Product -api -dc ChampContext -dbProvider postgres -outDir Controllers`

Как генерировать JWT:
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        };
    });


var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.Sub, userId.ToString()),
            new Claim(ClaimTypes.Role, roleId.ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: Configuration["Jwt:Issuer"],
            audience: Configuration["Jwt:Audience"],
            claims: claims ?? [],
            expires: DateTime.Now.AddDays(10),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);

и в ApiController [Authorization(Roles = "1,2")]
