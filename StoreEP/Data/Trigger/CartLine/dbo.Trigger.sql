CREATE TRIGGER [Trigger]
	ON [dbo].[CartLine]
	FOR INSERT
	AS
	BEGIN
		DECLARE @id int, @estoque int, @quantidade int
		SELECT @id = ProdutoID, @quantidade = Quantidade
		FROM INSERTED

	    SET @estoque = (SELECT Quantidade from Produto where ProdutoID = @id)
		IF @estoque != 0 AND @quantidade <= @estoque

		UPDATE Produto SET
		Quantidade =  @estoque - @quantidade 
		WHERE ProdutoID = @id
	END
