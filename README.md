# AsiaYo_Test

## 題目一 請寫出一條查詢語句 (SQL),列出在 2023 年 5 月下訂的訂單,使用台幣付款且 5 月總金額最多的前 10 筆的旅宿 ID (bnb_id), 旅宿名稱 (bnb_name), 5 月總金額 (may_amount)

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

1.  **使用 EXPLAIN**:

- 利用 `EXPLAIN` 關鍵字來查看 SQL 查詢的執行計劃，了解查詢過程中使用了哪些索引，以及可能的瓶頸所在。

2.  觀察查詢的執行時間，對比不同索引的效果。
3.  查看查詢日誌，了解是否存在長時間執行的查詢。

### 優化

1. 建立合適的索引:

- 根據查詢的 `WHERE` 條件和 `GROUP BY` 子句, 建立 mutiple column index, 其中的欄位順序應根據查詢頻率和欄位的獨特性決定 (`created_at` -> `currency` -> `bnb_id` -> `amount`)

- 在 `orders` 表上建立 covering index, 可以保證在查詢時只需訪問索引，不需再回查主表
  ```sql
  CREATE INDEX idx_orders_full ON orders (created_at, currency, bnb_id, amount);
  ```

2. 使用 Index Hints

- 使用 `USE INDEX` 或 `FORCE INDEX` 來指定使用的索引

## API 實作測驗

### SOLID 原則

- 單一職責原則（SRP）：將每個驗證邏輯分離到各自的策略類別中，讓每個類別只負責一種驗證邏輯
  <br> 1. `NameValidationStrategy`：只負責檢查名稱是否包含非英文字母以及是否每個單字的首字母大寫。
  <br> 2. `PriceValidationStrategy`：只負責檢查價格是否超過指定範圍。
  <br> 3. `CurrencyValidationStrategy`：只負責檢查貨幣格式是否正確，並在必要時進行價格轉換。
  <br> 4. `OrderReq` 類別只負責定義訂單的資料結構。
  <br> 5. `Order` 類別只負責處理訂單驗證的邏輯。
  <br>
- 開放封閉原則（OCP）：
  <br> 1. `對擴展開放`：要新增新的驗證邏輯，例如檢查訂單的日期或地址格式，只需創建一個新的策略類別，實作 `IOrderValidationStrategy` 介面，並將其添加到 \_validationStrategies 清單中即可。這樣，我們可以在不影響現有類別的情況下，輕鬆擴展系統的功能。
  <br> 2. `對修改封閉`：我們不需要修改現有的 `Order` 類別或任何現有的策略類別，就可以引入新的驗證邏輯。這降低了現有程式碼被意外破壞的風險，並提高了系統的穩定性。
  <br>
- 里氏替換原則（LSP）：
  <br> 1. 將驗證邏輯抽象成 `IOrderValidationStrategy` 介面並實作多個具體策略
  <br>
- 介面隔離原則（ISP）：
  <br> 1. `IOrderValidationStrategy` 介面只定義了 `Validate` 方法，這是所有策略類別需要實作的唯一方法。這樣每個策略類別只關心其特定的驗證邏輯，符合接口隔離原則。
  <br>
- 依賴倒轉原則（DIP）：
  <br> 1. `Order` 類別依賴於 `IOrderValidationStrategy` 介面而不是具體的策略實作。這樣高層模組（`Order` 類別）不依賴於低層模組（具體策略實作），而是依賴於抽象（`IOrderValidationStrategy` 介面）。
  <br> 2. `OrderController` 依賴於 `IOrder` 介面，而不是具體的 `Order` 實作，使得控制器可以更靈活地替換不同的訂單處理實作。

### 設計模式

- Strategy Pattern：
  `IOrderValidationStrategy` 介面來表示所有的驗證策略，並實作了不同的策略類別，如 `PriceValidationStrategy`、`NameValidationStrategy` 和 `CurrencyValidationStrategy`。
  <br>
- Dependency Injection：

1. 在 `OrderController` 中，`IOrder` 介面的實作（例如 Order 類別）被注入到控制器中，這使得 `OrderController` 可以依賴於抽象而不是具體實作。
   <br>
2. `Order` 類別內部依賴於 `IOrderValidationStrategy` 介面，使得具體的策略實作可以被注入或替換，而不需要修改 `Order` 類別的代碼。
