CREATE DATABASE "servico-curso"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Portuguese_Brazil.1252'
    LC_CTYPE = 'Portuguese_Brazil.1252'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;
	
CREATE SCHEMA IF NOT EXISTS public
    AUTHORIZATION pg_database_owner;

COMMENT ON SCHEMA public
    IS 'standard public schema';
	
GRANT USAGE ON SCHEMA public TO PUBLIC;

GRANT ALL ON SCHEMA public TO pg_database_owner;

DROP TABLE preco_curso IF EXISTS

-- Criação da tabela "curso"
CREATE TABLE curso (
    cod_curso SERIAL PRIMARY KEY,
    nome_curso VARCHAR(255) NOT NULL,
	descricao_curso VARCHAR(255),
    data_cadastro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    ativo BOOLEAN DEFAULT TRUE
);

DROP TABLE preco_curso IF EXISTS

-- Criação da tabela "preco_curso"
CREATE TABLE preco_curso (
    cod_preco_curso SERIAL PRIMARY KEY,
    cod_curso INTEGER NOT NULL,
	valor_curso DECIMAL(10,2) NOT NULL,
    data_cadastro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    ativo BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (cod_curso) REFERENCES curso (cod_curso)
);
CREATE INDEX idx_cod_curso ON preco_curso (cod_curso);
CREATE INDEX idx_cod_curso_ativo ON preco_curso (cod_curso, ativo);

-- Inserção de cursos na tabela "curso"
INSERT INTO curso (nome_curso, descricao_curso) VALUES
  ('Desenvolvimento Web', 'Curso para aprender desenvolvimento web.'),
  ('Banco de Dados', 'Curso para aprender conceitos de banco de dados.'),
  ('Redes de Computadores', 'Curso sobre redes de computadores e protocolos.'),
  ('Segurança da Informação', 'Curso para aprender sobre segurança da informação.'),
  ('Inteligência Artificial', 'Curso sobre inteligência artificial e machine learning.'),
  ('Análise de Dados', 'Curso para aprender análise de dados e estatística.'),
  ('Programação em Java', 'Curso para aprender programação em Java.'),
  ('Sistemas Operacionais', 'Curso sobre sistemas operacionais.'),
  ('Engenharia de Software', 'Curso para aprender engenharia de software.'),
  ('Machine Learning', 'Curso para aprender machine learning e algoritmos avançados.');

-- Inserção de preços na tabela "preco_curso"
INSERT INTO preco_curso (cod_curso, valor_curso) VALUES
  (1, 199.90),
  (2, 149.90),
  (3, 179.90),
  (4, 199.90),
  (5, 249.90),
  (6, 169.90),
  (7, 159.90),
  (8, 139.90),
  (9, 189.90),
  (10, 279.90);