resource "aws_ecs_task_definition" "kong" {
  family                = "kong-task"
  container_definitions = file("${path.module}/kong-container-definitions.json")
  network_mode          = "awsvpc"
  requires_compatibilities = ["FARGATE"]
  cpu                   = 512
  memory                = 1024
}

resource "aws_ecs_service" "kong" {
  name            = "kong-service"
  cluster         = aws_ecs_cluster.main.id
  task_definition = aws_ecs_task_definition.kong.arn
  desired_count   = 1
  launch_type     = "FARGATE"
  network_configuration {
    subnets         = var.subnet_ids
    security_groups = [var.security_group_id]
    assign_public_ip = true
  }
}