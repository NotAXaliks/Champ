create table "Statuses" (
    "Id" serial primary key,
    "Entity" text not null,
    "Name" text not null
);

create table "Roles" (
    "Id" serial primary key,
    "Name" text not null
);

create table "Suppliers" (
    "Id" serial primary key,
    "Name" text not null,
    "ContactInfo" text
);

create table "Departments" (
    "Id" serial primary key,
    "Name" text not null
);

create table "Users" (
    "Id" serial primary key,
    "Name" text not null,
    "RoleId" int not null references "Roles"("Id"),
    "DepartmentId" int not null references "Departments"("Id"),
    "Login" text not null,
    "Password" text not null
);

create table "ProductTypes" (
    "Id" serial primary key,
    "Name" text not null
);

create table "ProductForms" (
    "Id" serial primary key,
    "Name" text not null
);

create table "Products" (
    "Id" serial primary key,
    "Name" text not null,
    "Code" text not null unique,
    "FormId" int not null references "ProductForms"("Id"),
    "TypeId" int not null references "ProductTypes"("Id"),
    "StatusId" int not null references "Statuses"("Id")
);

create table "MaterialTypes" (
    "Id" serial primary key,
    "Name" text not null
);

create table "Units" (
    "Id" serial primary key,
    "Name" text not null
);

create table "Materials" (
    "Id" serial primary key,
    "Name" text not null,
    "Code" text not null,
    "TypeId" int not null references "MaterialTypes"("Id"),
    "StatusId" int not null references "Statuses"("Id")
);

create table "Recipes" (
    "Id" serial primary key,
    "ProductId" int not null references "Products"("Id"),
    "StatusId" int not null references "Statuses"("Id"),
    "Version" int not null,
    "ApprovedAt" timestamp,
    "ApprovedBy" int references "Users"("Id"),
    "CreatedAt" timestamp not null,
    "CreatedBy" int not null references "Users"("Id"),
    "Comment" text
);
create unique index on "Recipes"("ProductId") where "StatusId" = 1;

create table "RecipeComponents" (
    "Id" serial primary key,
    "RecipeId" int not null references "Recipes"("Id"),
    "Percentage" decimal(5, 2) not null,
    "Order" int not null,
    "MaterialId" int not null references "Materials"("Id")
);

create or replace function recipecomponents() returns trigger as $$
declare total numeric;
begin
    if new."StatusId" = 1 and old."StatusId" <> 1 then
        select sum("Percentage") into total
        from "RecipeComponents"
        where "RecipeId" = new."Id";

        if total <> 100 then
            raise exception 'Сумма компонентов должна быть 100%%';
        end if;
    end if;

    return new;
end;
$$ language plpgsql;

create trigger trg_recipecomponents
before update of "StatusId"
on "Recipes"
for each row
execute function recipecomponents();

create table "TechCards" (
    "Id" serial primary key,
    "RecipeId" int not null references "Recipes"("Id"),
    "StatusId" int not null references "Statuses"("Id"),
    "Version" int not null,
    "Description" text not null
);
create unique index on "TechCards"("RecipeId") where "StatusId" = 1;

create table "TechStepTypes" (
    "Id" serial primary key,
    "Name" text not null
);

create table "TechSteps" (
    "Id" serial primary key,
    "TypeId" int not null references "TechStepTypes"("Id"),
    "CardId" int not null references "TechCards"("Id"),
    "Name" text not null,
    "Description" text not null,
    "Required" boolean not null,
    "Status" int not null references "Statuses"("Id"),
    "Order" int not null
);

create table "TechStepParams" (
    "Id" serial primary key,
    "StepId" int not null references "TechSteps"("Id"),
    "Name" text not null,
    "MinValue" int not null,
    "MaxValue" int not null,
    "UnitId" int not null references "Units"("Id")
);

create table "MaterialBatches" (
    "Id" serial primary key,
    "MaterialId" int not null references "Materials"("Id"),
    "Code" text not null,
    "UnitId" int not null references "Units"("Id"),
    "Count" int not null,
    "Supplier" int not null references "Suppliers"("Id"),
    "Date" timestamp not null default now(),
    "StatusId" int not null references "Statuses"("Id")
);

create table "ProductOrders" (
    "Id" serial primary key,
    "ProductId" int not null references "Products"("Id"),
    "Code" text not null,
    "Count" int not null,
    "Date" timestamp not null default now(),
    "StatusId" int not null references "Statuses"("Id"),
    "CreatedBy" int not null references "Users"("Id")
);

create table "ProductBatches" (
    "Id" serial primary key,
    "OrderId" int not null references "ProductOrders"("Id"),
    "RecipeId" int not null references "Recipes"("Id"),
    "TechCardId" int not null references "TechCards"("Id"),
    "Code" text not null,
    "Count" int not null,
    "Date" timestamp not null default now(),
    "StatusId" int not null references "Statuses"("Id"),
    "CreatedAt" timestamp not null default now(),
    "EndedAt" timestamp
);

create table "BatchMaterials" (
    "Id" serial primary key,
    "BatchId" int not null references "ProductBatches"("Id"),
    "MaterialBatchId" int not null references "MaterialBatches"("Id"),
    "Count" int not null,
    "UnitId" int not null references "Units"("Id")
);

create table "BatchSteps" (
    "Id" serial primary key,
    "BatchId" int not null references "ProductBatches"("Id"),
    "TechStepId" int not null references "TechSteps"("Id"),
    "StatusId" int not null references "Statuses"("Id"),
    "StartedAt" timestamp,
    "FinishedAt" timestamp,
    "OperatorId" int references "Users"("Id"),
    "Comment" text
);

create table "BatchStepParams" (
    "Id" serial primary key,
    "BatchStepId" int not null references "BatchSteps"("Id"),
    "TechParamId" int not null references "TechStepParams"("Id"),
    "Value" int not null,
    "UnitId" int not null references "Units"("Id"),
    "IsOutOfRange" boolean not null
);

create table "LabTests" (
    "Id" serial primary key,
    "MaterialBatchId" int references "MaterialBatches"("Id"),
    "ProductBatchId" int references "ProductBatches"("Id"),
    "Type" text not null,
    "Priority" int not null,
    "StatusId" int not null references "Statuses"("Id"),
    "CreatedAt" timestamp not null default now(),
    "CreatedBy" int not null references "Users"("Id"),
    "CompletedAt" timestamp,
    "Comment" text
);

create table "LabParams" (
    "Id" serial primary key,
    "Name" text not null
);

create table "LabTestParams" (
    "Id" serial primary key,
    "TestId" int not null references "LabTests"("Id"),
    "ParamId" int not null references "LabParams"("Id"),
    "MinValue" int,
    "MaxValue" int,
    "ActualValue" int,
    "UnitId" int not null references "Units"("Id"),
    "IsOk" boolean
);

create table "LabDecisions" (
    "Id" serial primary key,
    "TestId" int not null references "LabTests"("Id"),
    "Decision" text not null, -- APPROVED / REJECTED
    "Comment" text,
    "CreatedAt" timestamp not null default now(),
    "CreatedBy" int not null references "Users"("Id")
);

create table "Deviations" (
    "Id" serial primary key,
    "BatchStepParamId" int references "BatchStepParams"("Id"),
    "BatchId" int references "ProductBatches"("Id"),
    "ActualValue" int,
    "Severity" text, -- INFO / WARNING / CRITICAL
    "Comment" text,
    "CreatedAt" timestamp not null default now(),
    "CreatedBy" int not null references "Users"("Id")
);

create table "Events" (
    "Id" serial primary key,
    "EntityType" text not null, -- Batch, Step, Test
    "EntityId" int not null,
    "EventType" text not null, -- START, END, ERROR
    "CreatedAt" timestamp not null default now(),
    "CreatedBy" int references "Users"("Id"),
    "Data" text
);

create table "Equipments" (
    "Id" serial primary key,
    "Name" text not null,
    "Type" text not null,
    "StatusId" int not null references "Statuses"("Id")
);

create table "EquipmentTelemetry" (
    "Id" serial primary key,
    "EquipmentId" int not null references "Equipments"("Id"),
    "BatchId" int references "ProductBatches"("Id"),
    "Parameter" text not null,
    "Value" int not null,
    "CreatedAt" timestamp not null default now()
);

create table "StatusHistory" (
    "Id" serial primary key,
    "EntityType" text not null,
    "EntityId" int not null,
    "OldStatusId" int references "Statuses"("Id"),
    "NewStatusId" int references "Statuses"("Id"),
    "ChangedAt" timestamp not null default now(),
    "ChangedBy" int references "Users"("Id")
);
