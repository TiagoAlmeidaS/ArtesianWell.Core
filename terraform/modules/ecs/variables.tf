variable "cluster_name" {
  description = "The name of the ECS cluster"
  type        = string
}

variable "family" {
  description = "The family name for the task definition"
  type        = string
}

variable "container_definitions_file" {
  description = "The file containing the container definitions"
  type        = string
}

variable "cpu" {
  description = "The number of cpu units used by the task"
  type        = number
}

variable "memory" {
  description = "The amount of memory (in MiB) used by the task"
  type        = number
}

variable "service_name" {
  description = "The name of the ECS service"
  type        = string
}

variable "desired_count" {
  description = "The desired number of tasks to run"
  type        = number
}

variable "subnet_ids" {
  description = "The subnet IDs for the ECS service"
  type        = list(string)
}

variable "security_group_id" {
  description = "The security group ID for the ECS service"
  type        = string
}