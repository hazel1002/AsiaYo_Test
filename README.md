# AsiaYo_Test

## 題目一 請寫出一條查詢語句 (SQL),列出在 2023 年 5 月下訂的訂單,使用台幣付款且5月總金額最多的前 10 筆的旅宿 ID (bnb_id), 旅宿名稱 (bnb_name), 5 月總金額 (may_amount)

```sql
 SELECT 
    bnbs.id AS bnb_id,
    bnbs.name AS bnb_name,
    SUM(orders.amount) AS may_amount
FROM 
    orders
JOIN 
    bnbs ON orders.bnb_id = bnbs.id
WHERE 
    orders.currency = 'TWD' AND
    orders.created_at >= '2023-05-01' AND 
    orders.created_at < '2023-06-01'
GROUP BY 
    bnbs.id, bnbs.name
ORDER BY 
    may_amount DESC
LIMIT 10; 
```


## 題目二 在題目一的執行下,我們發現 SQL 執行速度很慢,您會怎麼去優化?請闡述您怎麼判斷與優化的方式


### 如何判斷
 1. **使用 EXPLAIN**:
   - 利用 `EXPLAIN` 關鍵字來查看 SQL 查詢的執行計劃，了解查詢過程中使用了哪些索引，以及可能的瓶頸所在。
 2. 觀察查詢的執行時間，對比不同索引的效果。
 3. 查看查詢日誌，了解是否存在長時間執行的查詢。

### 優化

1. **建立合適的索引**:
   - 在 `orders` 表上建立複合索引：
     ```sql
     CREATE INDEX idx_orders_full ON orders (created_at, currency, bnb_id, amount);
     ```
2. 使用索引提示（Index Hints）
 - 使用 `USE INDEX` 或 `FORCE INDEX` 來指定使用的索引





