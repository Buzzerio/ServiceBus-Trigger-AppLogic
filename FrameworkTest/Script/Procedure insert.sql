/****** Object:  StoredProcedure [dbo].[Test_Insert_Test_Trigger]    Script Date: 14/06/2019 18:00:20 ******/
DROP PROCEDURE [dbo].[Test_Insert_Test_Trigger]
GO

/****** Object:  StoredProcedure [dbo].[Test_Insert_Test_Trigger]    Script Date: 14/06/2019 18:00:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[Test_Insert_Test_Trigger]
(
    -- Add the parameters for the stored procedure here
    @campo nchar(50),
	@valore int
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    Insert into Test_Trigger
	values (@campo, @valore);
END
GO


