USE master;
CREATE DATABASE TechShopInventory;
GO

USE TechShopInventory;

CREATE TABLE Warehouses(
    IdWarehouse UNIQUEIDENTIFIER NOT NULL,
	Code NVARCHAR(20) NOT NULL,
	Name NVARCHAR(100) NOT NULL,
	Location NVARCHAR(200) NOT NULL,

	CONSTRAINT PK_Warehouses PRIMARY KEY (IdWarehouse),
	CONSTRAINT UQ_Warehoses_Code UNIQUE(Code)
);

CREATE TABLE StockItems(
	IdStockItem UNIQUEIDENTIFIER NOT NULL,
	IdWarehouse UNIQUEIDENTIFIER NOT NULL,
	Sku NVARCHAR(50) NOT NULL,
	QuantityAvailable INT NOT NULL,
	QuantityReserved INT NOT NULL,
	QuantityTotal AS (QuantityAvailable + QuantityReserved) PERSISTED,
	LastUpdated DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

	CONSTRAINT PK_StockItems PRIMARY KEY (IdStockItem),
	CONSTRAINT FK_StockItems_Warehouses_IdWarehouse FOREIGN KEY (IdWarehouse) REFERENCES Warehouses(IdWarehouse),
	CONSTRAINT UQ_StockItems_IdWarehouse_Sku UNIQUE(IdWarehouse, Sku),
	CONSTRAINT CK_StockItems_Quantity CHECK (QuantityAvailable >= 0 AND QuantityReserved >= 0 )
);

CREATE TABLE InventoryMovements(
	IdInventoryMovement INT IDENTITY,
	IdStockItem UNIQUEIDENTIFIER NOT NULL,
	MovementType NVARCHAR(20) NOT NULL, 				-- entrada, salida, reserva, liberacion, ajuste
	Quantity INT NOT NULL,
	CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
	Reason NVARCHAR(200), 								-- Compra, Devolucion, consiliacion, etc
	ReferenceId NVARCHAR(50), 							-- Codigo de orden, codigo de autorizacion

	CONSTRAINT CK_Movements_MovementType CHECK (MovementType IN ('IN', 'OUT', 'RESERVE', 'RELEASE', 'ADJUST')),
	CONSTRAINT CK_Movements_Quantity CHECK (Quantity > 0),
	CONSTRAINT PK_InventoryMovements PRIMARY KEY (IdInventoryMovement),
	CONSTRAINT FK_InventoryMovements_StockItems_IdStockItem FOREIGN KEY (IdStockItem) REFERENCES StockItems(IdStockItem)
);

CREATE TABLE StockReservations (
    IdStockReservation UNIQUEIDENTIFIER NOT NULL,
    IdStockItem UNIQUEIDENTIFIER NOT NULL,
    Quantity INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    ExpiresAt DATETIME2 NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'PENDING',
    Reason NVARCHAR(200),
    ReferenceId NVARCHAR(50),            

	CONSTRAINT PK_StockReservations PRIMARY KEY(IdStockReservation),
	CONSTRAINT CK_StockReservations_Status CHECK (Status IN ('PENDING', 'CONFIRMED', 'CANCELLED', 'EXPIRED')),
	CONSTRAINT CK_StockReservations_Quantity CHECK( Quantity > 0 ),
    CONSTRAINT FK_StockReservations_StockItems FOREIGN KEY (IdStockItem) REFERENCES StockItems(IdStockItem)
);
