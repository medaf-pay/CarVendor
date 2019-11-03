namespace CarVendor.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V000 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarCategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CarId = c.Long(nullable: false),
                        CategoryId = c.Long(nullable: false),
                        EngineCapacity = c.Int(nullable: false),
                        TransfersNo = c.Int(nullable: false),
                        CylindersNo = c.Int(nullable: false),
                        CastingsNo = c.Int(nullable: false),
                        ElectronicFuelInjection = c.Boolean(nullable: false),
                        MaximumTorque = c.String(),
                        EnginePower = c.String(),
                        Acceleration = c.Int(nullable: false),
                        TractionType = c.String(),
                        SeatsNo = c.Int(nullable: false),
                        DoorsNo = c.Int(nullable: false),
                        AvgFuelConsumption = c.Int(nullable: false),
                        FuelTankCapacity = c.Int(nullable: false),
                        GroundClearance = c.Int(nullable: false),
                        MaxSpeed = c.Int(nullable: false),
                        FuelRecommended = c.String(),
                        DriverAirbags = c.Boolean(nullable: false),
                        FrontPassengerAirbags = c.Boolean(nullable: false),
                        ElectricChairs = c.Boolean(nullable: false),
                        BrakeSystemABS = c.Boolean(nullable: false),
                        ElectronicBrakeDistribution = c.Boolean(nullable: false),
                        ElectronicBalanceProgram = c.Boolean(nullable: false),
                        AntitheftAlarmSystem = c.Boolean(nullable: false),
                        ImmobilizerSystemAgainstTheft = c.Boolean(nullable: false),
                        SportRims = c.Boolean(nullable: false),
                        RimSize = c.Int(nullable: false),
                        FrontFogLanterns = c.Boolean(nullable: false),
                        BackFogLanterns = c.Boolean(nullable: false),
                        BackCleaners = c.Boolean(nullable: false),
                        ElectricSideMirrors = c.Boolean(nullable: false),
                        ElectricallyFoldingSideMirrors = c.Boolean(nullable: false),
                        SideMirrorsSignals = c.Boolean(nullable: false),
                        XenonBulbsLighting = c.Boolean(nullable: false),
                        HeadlampWipers = c.Boolean(nullable: false),
                        SensitiveHeadlamps = c.Boolean(nullable: false),
                        HeadlampControl = c.Boolean(nullable: false),
                        HeadlampLightingLED = c.Boolean(nullable: false),
                        TaillightsLightingLED = c.Boolean(nullable: false),
                        BackSpoiler = c.Boolean(nullable: false),
                        IntelligentParkingSystem = c.Boolean(nullable: false),
                        SoundSystem = c.Boolean(nullable: false),
                        CDDriver = c.Boolean(nullable: false),
                        AUXPort = c.Boolean(nullable: false),
                        USBPort = c.Boolean(nullable: false),
                        Bluetooth = c.Boolean(nullable: false),
                        FrontHeadrests = c.Boolean(nullable: false),
                        RearHeadrests = c.Boolean(nullable: false),
                        ElectricWindshield = c.Boolean(nullable: false),
                        ElectricRearGlass = c.Boolean(nullable: false),
                        OneTouchGlassControl = c.Boolean(nullable: false),
                        RemoteControlToLockAndOpenDoors = c.Boolean(nullable: false),
                        DriverHeightControl = c.Boolean(nullable: false),
                        LeatherBrushes = c.Boolean(nullable: false),
                        EngineStartStopButtonSystem = c.Boolean(nullable: false),
                        Sunroof = c.Boolean(nullable: false),
                        ElectricSunroof = c.Boolean(nullable: false),
                        BackCamera = c.Boolean(nullable: false),
                        ComputerTrips = c.Boolean(nullable: false),
                        SteeringWheelType = c.Boolean(nullable: false),
                        ControllableSteeringWheel = c.Boolean(nullable: false),
                        ControlTheSoundSystemOfTheSteeringWheel = c.Boolean(nullable: false),
                        CruiseControl = c.Boolean(nullable: false),
                        LeatherSteeringWheel = c.Boolean(nullable: false),
                        LeatherTransmission = c.Boolean(nullable: false),
                        FrontDoorStorage = c.Boolean(nullable: false),
                        BackDoorStorageAreas = c.Boolean(nullable: false),
                        PossibilityToFoldBackSeats = c.Boolean(nullable: false),
                        Lighter = c.Boolean(nullable: false),
                        MobileAshtray = c.Boolean(nullable: false),
                        CentralDoorLock = c.Boolean(nullable: false),
                        AlarmSoundWhenTheCarIsNotClosed = c.Boolean(nullable: false),
                        FrontCupHolder = c.Boolean(nullable: false),
                        BackCupHolder = c.Boolean(nullable: false),
                        FrontArmrest = c.Boolean(nullable: false),
                        AirConditionedFrontArmrest = c.Boolean(nullable: false),
                        BackArmrest = c.Boolean(nullable: false),
                        BackTrunkCover = c.Boolean(nullable: false),
                        FrontStorageDrawer = c.Boolean(nullable: false),
                        PowerOutlet = c.Boolean(nullable: false),
                        BackOutletPowerOutlet = c.Boolean(nullable: false),
                        BackWipers = c.Boolean(nullable: false),
                        RainSensitiveWindshieldWipers = c.Boolean(nullable: false),
                        BackLight = c.Boolean(nullable: false),
                        SensitiveHeadlampsForLighting = c.Boolean(nullable: false),
                        BackTrunkSpace = c.Boolean(nullable: false),
                        BackSeatBelt = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CarId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Model = c.String(),
                        Condition = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        BrandId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.CarFamilies", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.TypeId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.CarFamilies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarColors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CarCategoryId = c.Long(nullable: false),
                        ColorId = c.Long(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarCategories", t => t.CarCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Colors", t => t.ColorId, cascadeDelete: true)
                .Index(t => t.CarCategoryId)
                .Index(t => t.ColorId);
            
            CreateTable(
                "dbo.CarImages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ImageURL = c.String(),
                        CarColorId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarColors", t => t.CarColorId, cascadeDelete: true)
                .Index(t => t.CarColorId);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Link = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CarColor_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarColors", t => t.CarColor_Id)
                .Index(t => t.CarColor_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Conversions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FromCurrencyId = c.Long(),
                        ToCurrencyId = c.Long(),
                        CreationDate = c.DateTime(nullable: false),
                        Value = c.Int(nullable: false),
                        Plus = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Currency_Id = c.Long(),
                        Currency_Id1 = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currencies", t => t.Currency_Id)
                .ForeignKey("dbo.Currencies", t => t.Currency_Id1)
                .ForeignKey("dbo.Currencies", t => t.FromCurrencyId)
                .ForeignKey("dbo.Currencies", t => t.ToCurrencyId)
                .Index(t => t.FromCurrencyId)
                .Index(t => t.ToCurrencyId)
                .Index(t => t.Currency_Id)
                .Index(t => t.Currency_Id1);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Mails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        mail = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        OrderNumber = c.String(),
                        UserId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Color = c.String(),
                        Quantity = c.Int(nullable: false),
                        Category = c.String(),
                        CarId = c.Long(nullable: false),
                        OrderId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.CarId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Individually = c.Int(nullable: false),
                        FName = c.String(),
                        MName = c.String(),
                        LName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Phone = c.String(),
                        NationalId = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserAddresses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        AddressId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MainAddress = c.String(),
                        DeliveryAddress = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        userId = c.Long(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.userId)
                .Index(t => t.userId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "userId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.UserAddresses", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserAddresses", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Conversions", "ToCurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Conversions", "FromCurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Conversions", "Currency_Id1", "dbo.Currencies");
            DropForeignKey("dbo.Conversions", "Currency_Id", "dbo.Currencies");
            DropForeignKey("dbo.CarCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Videos", "CarColor_Id", "dbo.CarColors");
            DropForeignKey("dbo.CarColors", "ColorId", "dbo.Colors");
            DropForeignKey("dbo.CarImages", "CarColorId", "dbo.CarColors");
            DropForeignKey("dbo.CarColors", "CarCategoryId", "dbo.CarCategories");
            DropForeignKey("dbo.Cars", "TypeId", "dbo.CarFamilies");
            DropForeignKey("dbo.CarCategories", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Cars", "BrandId", "dbo.Brands");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "userId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.UserAddresses", new[] { "AddressId" });
            DropIndex("dbo.UserAddresses", new[] { "UserId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.OrderItems", new[] { "CarId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Conversions", new[] { "Currency_Id1" });
            DropIndex("dbo.Conversions", new[] { "Currency_Id" });
            DropIndex("dbo.Conversions", new[] { "ToCurrencyId" });
            DropIndex("dbo.Conversions", new[] { "FromCurrencyId" });
            DropIndex("dbo.Videos", new[] { "CarColor_Id" });
            DropIndex("dbo.CarImages", new[] { "CarColorId" });
            DropIndex("dbo.CarColors", new[] { "ColorId" });
            DropIndex("dbo.CarColors", new[] { "CarCategoryId" });
            DropIndex("dbo.Cars", new[] { "BrandId" });
            DropIndex("dbo.Cars", new[] { "TypeId" });
            DropIndex("dbo.CarCategories", new[] { "CategoryId" });
            DropIndex("dbo.CarCategories", new[] { "CarId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Addresses");
            DropTable("dbo.UserAddresses");
            DropTable("dbo.Users");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
            DropTable("dbo.Mails");
            DropTable("dbo.Currencies");
            DropTable("dbo.Conversions");
            DropTable("dbo.Categories");
            DropTable("dbo.Videos");
            DropTable("dbo.Colors");
            DropTable("dbo.CarImages");
            DropTable("dbo.CarColors");
            DropTable("dbo.CarFamilies");
            DropTable("dbo.Cars");
            DropTable("dbo.CarCategories");
            DropTable("dbo.Brands");
        }
    }
}
