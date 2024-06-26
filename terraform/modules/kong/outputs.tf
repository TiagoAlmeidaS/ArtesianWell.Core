output "ecs_service_id" {
  value = aws_ecs_service.kong.id
}

output "ecs_task_definition_arn" {
  value = aws_ecs_task_definition.kong.arn
}