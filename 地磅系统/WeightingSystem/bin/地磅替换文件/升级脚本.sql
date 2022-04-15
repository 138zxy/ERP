
/****** Object:  View [dbo].[MetageStuffIn]    Script Date: 2020/5/8 14:37:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER  VIEW [dbo].[MetageStuffIn]
AS
    SELECT  *
    FROM    ( SELECT    ROW_NUMBER() OVER ( PARTITION BY father.StuffInID ORDER BY child.ParentID, child.OrderNum DESC ) AS rownumber ,
                        father.StuffInID ,
                        father.StuffID ,
                        father.SiloID ,
                        father.SupplyID ,
                        father.TotalNum ,
                        ( CASE WHEN child.CarWeight IS NULL
                               THEN father.CarWeight
                               ELSE child.CarWeight
                          END ) AS carweight ,
                        ( CASE WHEN child.CarWeight IS NULL THEN father.InNum
                               WHEN child.InNum = 0 THEN 0
                               ELSE father.TotalNum - child.CarWeight
                          END ) AS Innum ,
                        father.SourceAddr ,
                        father.InDate ,
                        ( CASE WHEN child.CarWeight IS NULL
                               THEN father.OutDate
                               ELSE child.OutDate
                          END ) AS OutDate ,
                        father.AH ,
                        father.Remark ,
                        father.CarNo ,
                        father.Operator ,
                        father.FootStatus ,
                        father.BuildTime ,
                        father.Lifecycle ,
                        father.pic1 ,
                        father.pic2 ,
                        ( CASE WHEN child.CarWeight IS NULL THEN father.pic3
                               ELSE child.pic3
                          END ) AS pic3 ,
                        ( CASE WHEN child.CarWeight IS NULL THEN father.pic4
                               ELSE child.pic4
                          END ) AS pic4 ,
                        father.TransportID ,
                        father.WRate ,
                        father.Proportion ,
                        father.Driver ,
                        father.DarkWeight ,
                        father.Spec ,
                        father.FastMetage ,
                        father.ParentID ,
                        father.OrderNum ,
                        father.Builder ,
                        father.MingWeight ,
                        father.CompanyID ,
                        father.SourceNumber,
                        father.SupplyNum + ISNULL(child.SupplyNum,0.00) AS SupplyNum 
						,Com.CompName
						,father.Volume
              FROM      dbo.StuffIn AS father
                        LEFT JOIN StuffIn AS child ON child.ParentID = father.StuffInID
                                                      AND child.Lifecycle <> 2
						LEFT JOIN Company Com ON father.CompanyID=Com.CompanyID
              WHERE     father.ParentID IS NULL
                        OR LTRIM(RTRIM(father.ParentID)) = ''
                        AND father.Lifecycle <> 2
            ) a
    WHERE   rownumber = 1;

GO


