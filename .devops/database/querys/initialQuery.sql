CREATE TABLE IF NOT EXISTS "Services"
(
    "Id"          text                     NOT NULL,
    "Available"   boolean                  NOT NULL,
    "Description" text                     NOT NULL,
    "Title"       text                     NOT NULL,
    "UpdatedAt"   timestamp with time zone NOT NULL DEFAULT now(),
    "CreatedAt"   timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_Services" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "OrderServices"
(
    "Id"                    TEXT PRIMARY KEY,
    "Status"                TEXT      NOT NULL,
    "ClientId"              TEXT      NOT NULL,
    "ServiceId"             TEXT      NOT NULL REFERENCES "Services" ("Id"),
    "BudgetSchedulingDate"  TIMESTAMP NOT NULL,
    "ServiceSchedulingDate" TIMESTAMP,
    "UpdatedAt"             TIMESTAMP WITH TIME ZONE DEFAULT now(),
    "CreatedAt"             TIMESTAMP WITH TIME ZONE DEFAULT now()
);


CREATE TABLE IF NOT EXISTS "OrderStatus"
(
    "Id"              INT PRIMARY KEY,
    "Name"            VARCHAR(50)              NOT NULL,
    "Description"     TEXT                     NOT NULL,
    "PossibleActions" TEXT                     NOT NULL,
    "UpdatedAt"       timestamp with time zone NOT NULL DEFAULT now(),
    "CreatedAt"       timestamp with time zone NOT NULL DEFAULT now()
);

-- Inserção dos dados na tabela OrderStatus
INSERT INTO "OrderStatus" ("Id", "Name", "Description", "PossibleActions")
VALUES (1, 'Solicitado', 'O cliente enviou uma solicitação de orçamento e está aguardando a resposta.',
        'Aguardando resposta do orçamento.'),
       (2, 'Orçamento Enviado', 'O orçamento foi gerado e enviado para o cliente, que agora pode aprovar ou rejeitar.',
        'Aguardar aprovação do cliente.'),
       (3, 'Orçamento Aprovado', 'O cliente aprovou o orçamento. Próxima etapa é o agendamento do serviço.',
        'Cliente deve selecionar a data de execução do serviço.'),
       (4, 'Data Selecionada',
        'O cliente escolheu uma data para a execução do serviço. O serviço será agendado para essa data.',
        'Preparar para execução do serviço na data escolhida.'),
       (5, 'Aguardando Pagamento', 'Aguardando o pagamento do cliente para confirmar a execução do serviço.',
        'Cliente deve realizar o pagamento.'),
       (6, 'Pagamento Confirmado', 'O pagamento foi confirmado e o serviço está agendado para a data escolhida.',
        'Preparar equipe técnica para execução do serviço.'),
       (7, 'Em Execução', 'O serviço está sendo executado pela equipe técnica.',
        'Monitorar progresso da execução do serviço.'),
       (8, 'Concluído', 'O serviço foi concluído com sucesso.', 'Notificar o cliente sobre a conclusão do serviço.'),
       (9, 'Cancelado', 'A ordem de serviço foi cancelada por alguma razão (ex: cliente desistiu, problemas técnicos).',
        'Nenhuma ação adicional necessária.'),
       (10, 'Orçamento Rejeitado', 'O cliente rejeitou o orçamento proposto.', 'Nenhuma ação adicional necessária.');

CREATE TABLE IF NOT EXISTS "Budgets"
(
    "Id"                 TEXT PRIMARY KEY,
    "OrderServiceId"     TEXT      NOT NULL,
    "Status"             TEXT      NOT NULL,
    "DescriptionService" TEXT      NOT NULL,
    "DateChoose"         TIMESTAMP NOT NULL,
    "DateAccepted"       TIMESTAMP NOT NULL,
    "TotalValue"         DECIMAL   NOT NULL,
    "UpdatedAt"          TIMESTAMP WITH TIME ZONE DEFAULT (CURRENT_TIMESTAMP AT TIME ZONE 'UTC'),
    "CreatedAt"          TIMESTAMP WITH TIME ZONE DEFAULT (CURRENT_TIMESTAMP AT TIME ZONE 'UTC')
);