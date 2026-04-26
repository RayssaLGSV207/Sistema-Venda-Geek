-- 2. TABELA CLIENTE
CREATE TABLE Cliente (
    idcliente INTEGER PRIMARY KEY AUTOINCREMENT,
    nome VARCHAR(50) NOT NULL,
    cpf CHAR(11) UNIQUE,
    rg VARCHAR(15),
    data_nascimento DATE,  -- Nome corrigido
    genero CHAR(1),
    profissao VARCHAR(30),
    nacionalidade VARCHAR(30),
    logradouro VARCHAR(30),
    numero_resid VARCHAR(10),
    complemento VARCHAR(30),
    bairro VARCHAR(30),
    municipio VARCHAR(30),
    uf VARCHAR(2),  -- UF tem 2 caracteres (SP, RJ, MG)
    observacoes TEXT
);