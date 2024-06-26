variable "allocated_storage" {
  description = "The allocated storage in gigabytes"
  type        = number
  default     = 20
}

variable "instance_class" {
  description = "The instance class for the database"
  type        = string
  default     = "db.t2.micro"
}

variable "db_name" {
  description = "The name of the database"
  type        = string
}

variable "username" {
  description = "The username for the database"
  type        = string
}

variable "password" {
  description = "The password for the database"
  type        = string
}

variable "subnet_ids" {
  description = "The subnet IDs for the database"
  type        = list(string)
}

variable "security_group_id" {
  description = "The security group ID for the database"
  type        = string
}