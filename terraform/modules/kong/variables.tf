variable "subnet_ids" {
  description = "The subnet IDs for the ECS service"
  type        = list(string)
}

variable "security_group_id" {
  description = "The security group ID for the ECS service"
  type        = string
}