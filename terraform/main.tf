provider "aws" {
  region = "us-east-1"
}

module "vpc" {
  source = "./modules/vpc"
}

module "rds" {
  source = "./modules/rds"
  allocated_storage = 20
  instance_class    = "db.t2.micro"
  db_name           = "keycloakdb"
  username          = "admin"
  password          = "password"
  subnet_ids        = module.vpc.public_subnets
  security_group_id = module.vpc.default_security_group_id
}

module "ecs" {
  source                    = "./modules/ecs"
  cluster_name              = "main-cluster"
  family                    = "service-family"
  container_definitions_file = "path/to/container-definitions.json"
  cpu                       = 256
  memory                    = 512
  service_name              = "service-name"
  desired_count             = 1
  subnet_ids                = module.vpc.public_subnets
  security_group_id         = module.vpc.default_security_group_id
}

module "kong" {
  source           = "./modules/kong"
  subnet_ids       = module.vpc.public_subnets
  security_group_id = module.vpc.default_security_group_id
}