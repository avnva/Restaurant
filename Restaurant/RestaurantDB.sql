CREATE TABLE "Roles" (
    "RoleID" SERIAL PRIMARY KEY,
    "RoleName" VARCHAR(50) NOT NULL
);

CREATE TABLE "AccessRights" (
    "AccessRightID" SERIAL PRIMARY KEY,
    "AccessRightName" VARCHAR(50) NOT NULL
);

CREATE TABLE "Users" (
    "UserID" SERIAL PRIMARY KEY,
    "Username" VARCHAR(50) NOT NULL,
    "PasswordHash" VARCHAR(255) NOT NULL,
    "RoleID" INT
);

CREATE TABLE "RolesRights" (
    "RoleID" INT,
    "AccessRightID" INT
);

CREATE TABLE "DishGroups" (
    "GroupID" SERIAL PRIMARY KEY,
    "GroupName" VARCHAR(100) NOT NULL
);

CREATE TABLE "Products" (
    "ProductID" SERIAL PRIMARY KEY,
    "ProductName" VARCHAR(100) NOT NULL,
    "UnitsOfMeasureID" INT,
    "PriceMarkup" DECIMAL(10,2)
);

CREATE TABLE "UnitsOfMeasure" (
    "UnitsOfMeasureID" SERIAL PRIMARY KEY,
    "UnitsOfMeasureName" VARCHAR(20)
);

CREATE TABLE "Suppliers" (
    "SupplierID" SERIAL PRIMARY KEY,
    "SupplierName" VARCHAR(100) NOT NULL,
    "Address" VARCHAR(255),
    "ContactPersonName" VARCHAR(100),
    "Phone" VARCHAR(20),
    "BankName" VARCHAR(100),
    "BankAccount" VARCHAR(50),
    "INN" VARCHAR(20)
);

CREATE TABLE "Departments" (
    "DepartmentID" SERIAL PRIMARY KEY,
    "DepartmentName" VARCHAR(100) NOT NULL,
    "ActionsDescription" TEXT
);

CREATE TABLE "DepartmentsProducts" (
    "DepartmentID" INT,
    "ProductID" INT,
    "Quantity" FLOAT
);

CREATE TABLE "Menu" (
    "DishInMenuID" SERIAL PRIMARY KEY,
    "DishID" INT,
    "StatusID" INT,
    "Comment" TEXT
);

CREATE TABLE "Statuses" (
    "StatusID" SERIAL PRIMARY KEY,
    "StatusName" VARCHAR(20)
);

CREATE TABLE "Dishes" (
    "DishID" SERIAL PRIMARY KEY,
    "GroupID" INT,
    "DishName" VARCHAR(100) NOT NULL,
    "DishCost" DECIMAL(10,2),
    "OutputWeight" FLOAT,
    "CookingTechnology" TEXT,
    "Photo" VARCHAR(100)
);

CREATE TABLE "DishesProducts" (
    "DishID" INT,
    "ProductID" INT,
    "Quantity" FLOAT
);

CREATE TABLE "Warehouses" (
    "WarehouseID" SERIAL PRIMARY KEY,
    "ProductID" INT,
    "StockBalance" FLOAT,
    "SupplierID" INT
);

CREATE TABLE "Supplies" (
    "SupplyID" SERIAL PRIMARY KEY,
    "SupplierID" INT,
    "SupplyDate" TIMESTAMP,
    "PurchasePrice" DECIMAL(10,2)
);

CREATE TABLE "SuppliesProducts" (
    "SupplyID" INT,
    "ProductID" INT,
    "DeliveredQuantity" FLOAT
);

CREATE TABLE "Requests" (
    "RequestID" SERIAL PRIMARY KEY,
    "DepartmentID" INT,
    "RequestDate" TIMESTAMP
);


CREATE TABLE "RequestsProducts" (
    "RequestID" INT,
    "ProductID" INT,
    "Quantity" FLOAT
);

CREATE TABLE "Orders" (
    "OrderID" SERIAL PRIMARY KEY,
    "OrderDate" TIMESTAMP,
    "OrderCost" DECIMAL(10,2)
);

CREATE TABLE "OrdersDishes" (
    "DishID" INT,
    "OrderID" INT,
    "Quantity" INT
);

ALTER TABLE "Users"ADD FOREIGN KEY ("RoleID") REFERENCES "Roles"("RoleID");

ALTER TABLE "RolesRights" ADD FOREIGN KEY ("RoleID") REFERENCES "Roles"("RoleID");

ALTER TABLE "RolesRights" ADD FOREIGN KEY ("AccessRightID") REFERENCES "AccessRights"("AccessRightID");

ALTER TABLE "Products" ADD FOREIGN KEY ("UnitsOfMeasureID") REFERENCES "UnitsOfMeasure" ("UnitsOfMeasureID");

ALTER TABLE "Menu" ADD FOREIGN KEY ("DishID") REFERENCES "Dishes" ("DishID");

ALTER TABLE "Menu" ADD FOREIGN KEY ("StatusID") REFERENCES "Statuses" ("StatusID");

ALTER TABLE "Dishes" ADD FOREIGN KEY ("GroupID") REFERENCES "DishGroups" ("GroupID");

ALTER TABLE "DishesProducts" ADD FOREIGN KEY ("DishID") REFERENCES "Dishes" ("DishID");

ALTER TABLE "DishesProducts" ADD FOREIGN KEY ("ProductID") REFERENCES "Products" ("ProductID");

ALTER TABLE "OrdersDishes" ADD FOREIGN KEY ("OrderID") REFERENCES "Orders" ("OrderID");

ALTER TABLE "OrdersDishes" ADD FOREIGN KEY ("DishID") REFERENCES "Dishes" ("DishID");

ALTER TABLE "DepartmentsProducts" ADD FOREIGN KEY ("DepartmentID") REFERENCES "Departments" ("DepartmentID");

ALTER TABLE "DepartmentsProducts" ADD FOREIGN KEY ("ProductID") REFERENCES "Products" ("ProductID");

ALTER TABLE "Warehouses" ADD FOREIGN KEY ("ProductID") REFERENCES "Products" ("ProductID");

ALTER TABLE "Warehouses" ADD FOREIGN KEY ("SupplierID") REFERENCES "Suppliers" ("SupplierID");

ALTER TABLE "Supplies" ADD FOREIGN KEY ("SupplierID") REFERENCES "Suppliers" ("SupplierID");

ALTER TABLE "SuppliesProducts" ADD FOREIGN KEY ("SupplyID") REFERENCES "Supplies" ("SupplyID");

ALTER TABLE "SuppliesProducts" ADD FOREIGN KEY ("ProductID") REFERENCES "Products" ("ProductID");

ALTER TABLE "Requests" ADD FOREIGN KEY ("DepartmentID") REFERENCES "Departments" ("DepartmentID");

ALTER TABLE "RequestsProducts" ADD FOREIGN KEY ("RequestID") REFERENCES "Requests" ("RequestID");

ALTER TABLE "RequestsProducts" ADD FOREIGN KEY ("ProductID") REFERENCES "Products" ("ProductID");

ALTER TABLE "OrdersDishes" ADD FOREIGN KEY ("DishID") REFERENCES "Dishes" ("DishID");

ALTER TABLE "OrdersDishes" ADD FOREIGN KEY ("OrderID") REFERENCES "Orders" ("OrderID");
