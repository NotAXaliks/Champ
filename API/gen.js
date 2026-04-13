const { execSync } = require("child_process");

const models = ["Product", "ProductType", "ProductForm", "Department", "Material", "Status", "User", "Role", "Recipe", "Supplier"];

models.forEach((model) => {
    console.log(execSync(`dotnet aspnet-codegenerator controller -api -dc ChampionContext -dbProvider postgres -outDir Controllers -f -m ${model} -name ${model}Controller`).toString())
})
