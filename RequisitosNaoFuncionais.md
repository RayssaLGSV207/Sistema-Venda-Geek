# REQUISITOS NÃO FUNCIONAIS - SISTEMA VENDA GEEK

## 1. Usabilidade

- Interface intuitiva com botões grandes e coloridos
- Mensagens de confirmação para ações críticas (cancelamento, exclusão)
- Campos obrigatórios sinalizados com "*"
- Feedback visual ao passar o mouse sobre botões

## 2. Desempenho

- Tempo de resposta inferior a 2 segundos para consultas
- Banco de dados SQLite otimizado com índices nas chaves primárias

## 3. Confiabilidade

- Tratamento de exceções com try-catch em todas as operações de banco
- Validação de campos antes de salvar
- Backup automático do banco (.db)

## 4. Segurança

- Login com usuário e senha (3 níveis de acesso)
- Superfície de ataque reduzida (sistema desktop, sem rede)

## 5. Manutenibilidade

- Código organizado em camadas (Forms, Services, Database)
- Comentários XML nos métodos públicos
- Padrão de projeto Singleton para conexão com banco

## 6. Portabilidade

- Aplicação Windows Forms compilada para AnyCPU/x64
- Banco de dados SQLite (arquivo único, sem servidor)

## 7. Acessibilidade

- Atalhos de teclado (Alt + tecla sublinhada)
- Contraste adequado entre texto e fundo
- Fonte Arial de tamanho legível (10-18pt)