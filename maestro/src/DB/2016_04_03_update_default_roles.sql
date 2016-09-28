DECLARE @CustomerId int
DECLARE @CustomerUserRoleId1 uniqueidentifier -- Customer Admin
DECLARE @CustomerUserRoleId2 uniqueidentifier -- Manage All Patients
DECLARE @CustomerUserRoleId3 uniqueidentifier -- Manage Own Patients
DECLARE @CustomerUserRoleId4 uniqueidentifier -- Health Content Manager

-- Deleting ALL Current CustomerUserRolesPermissionsMappings
DELETE FROM CustomerUserRolesPermissionsMappings

DECLARE MY_CURSOR CURSOR 
  LOCAL STATIC READ_ONLY FORWARD_ONLY
FOR 
SELECT DISTINCT CustomerId 
FROM CustomerUsers

OPEN MY_CURSOR
FETCH NEXT FROM MY_CURSOR INTO @CustomerId
WHILE @@FETCH_STATUS = 0
BEGIN
    SELECT @CustomerUserRoleId1 = Id FROM CustomerUserRoles WHERE Name = 'Customer Admin' AND CustomerId = @CustomerId
	SELECT @CustomerUserRoleId2 = Id FROM CustomerUserRoles WHERE Name = 'Manage All Patients' AND CustomerId = @CustomerId
	SELECT @CustomerUserRoleId3 = Id FROM CustomerUserRoles WHERE Name = 'Manage Own Patients' AND CustomerId = @CustomerId
	SELECT @CustomerUserRoleId4 = Id FROM CustomerUserRoles WHERE Name = 'Health Content Manager' AND CustomerId = @CustomerId

    -- Adding CustomerUserRolesPermissionsMappings for current Customer

	-- Customer Admin
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 101)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 102)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 103)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 104)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 105)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 106)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 107)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 108)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 109)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 110)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 112)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 113)

	-- Manage All Patients
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 301)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 303)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 304)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 305)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 306)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 307)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 308)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 310)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 311)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 313)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 314)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 316)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 201)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 317)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 318)

	-- Manage Own Patients
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 302)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 303)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 304)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 305)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 306)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 307)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 308)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 310)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 311)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 313)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 314)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 316)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 201)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 317)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 318)

	-- Health Content Manager
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId4, 111)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId4, 202)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId4, 203)
	INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId4, 204)

    FETCH NEXT FROM MY_CURSOR INTO @CustomerId
END
CLOSE MY_CURSOR
DEALLOCATE MY_CURSOR

SELECT @CustomerUserRoleId1 = Id FROM CustomerUserRoles WHERE Name = 'Customer Admin' AND CustomerId Is NULL
SELECT @CustomerUserRoleId2 = Id FROM CustomerUserRoles WHERE Name = 'Manage All Patients' AND CustomerId Is NULL
SELECT @CustomerUserRoleId3 = Id FROM CustomerUserRoles WHERE Name = 'Manage Own Patients' AND CustomerId Is NULL
SELECT @CustomerUserRoleId4 = Id FROM CustomerUserRoles WHERE Name = 'Health Content Manager' AND CustomerId Is NULL

-- Adding CustomerUserRolesPermissionsMappings for default roles

-- Customer Admin
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 101)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 102)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 103)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 104)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 105)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 106)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 107)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 108)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 109)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 110)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 112)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId1, 113)

-- Manage All Patients
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 301)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 303)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 304)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 305)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 306)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 307)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 308)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 310)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 311)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 313)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 314)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 316)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 201)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 317)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId2, 318)

-- Manage Own Patients
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 302)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 303)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 304)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 305)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 306)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 307)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 308)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 310)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 311)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 313)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 314)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 316)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 201)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 317)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId3, 318)

-- Health Content Manager
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId4, 111)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId4, 202)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId4, 203)
INSERT CustomerUserRolesPermissionsMappings (Id, CustomerUserRoleId, PermissionCode) VALUES (NEWID(), @CustomerUserRoleId4, 204)