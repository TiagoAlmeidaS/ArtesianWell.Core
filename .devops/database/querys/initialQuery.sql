CREATE TABLE "Services" (
                            "Id" text NOT NULL,
                            "Available" boolean NOT NULL,
                            "Description" text NOT NULL,
                            "Title" text NOT NULL,
                            "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
                            "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
                            CONSTRAINT "PK_Services" PRIMARY KEY ("Id")
);