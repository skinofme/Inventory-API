USE Inventory;

INSERT INTO Warehouses (IdWarehouse, Code, Name, Location, IsActive)
VALUES ('f81d4fae-7dec-11d0-a765-00a0c91e6bf6', 'WH-001-A', 'Central', 'Guatemala, Guatemala City, Zone 1, 7th Street, 7th Avenue', 1);

SELECT TOP(10) * FROM Warehouses;