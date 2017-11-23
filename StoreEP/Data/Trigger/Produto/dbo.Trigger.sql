CREATE TRIGGER tr_Produto_Update
	ON [dbo].[Produto]
	FOR UPDATE
	AS
	BEGIN
		DECLARE @precoAntigo DECIMAL(18,2), @precoNovo DECIMAL(18,2) 
		DECLARE @ID int, @data datetime

		SELECT @precoAntigo = Preco 
		FROM DELETED

		SELECT @precoNovo = Preco, @data = GETDATE(), @ID = ProdutoID 
		FROM INSERTED
		IF @precoAntigo != @precoNovo
		INSERT INTO HistoricoPreco VALUES (@data, @precoAntigo, @precoNovo, @ID)
	END