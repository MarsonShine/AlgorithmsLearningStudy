package sorts;

import java.util.PriorityQueue;

class Trade {
    private double price;
    private int quantity;

    public Trade(double price, int quantity) {
        this.price = price;
        this.quantity = quantity;
    }

    /**
     * This method returns the price.
     * 
     * @return the price
     */
    public double getPrice() {
        // Return the price
        return price;
    }

    public void setPrice(double price) {
        this.price = price;
    }

    public int getQuantity() {
        return quantity;
    }

    /**
     * Sets the quantity of the object.
     *
     * @param quantity the new quantity value
     */
    public void setQuantity(int quantity) {
        this.quantity = quantity;
    }

}

public class StockTrading {

    public static void main(String[] args) {
        // 模拟买家和卖家
        PriorityQueue<Trade> buyers = new PriorityQueue<>((a, b) -> Double.compare(a.getPrice(), b.getPrice()));
        PriorityQueue<Trade> sellers = new PriorityQueue<>((a, b) -> Double.compare(b.getPrice(), a.getPrice()));

        // 添加模拟数据
        buyers.add(new Trade(1.0, 10));
        buyers.add(new Trade(2.0, 20));
        buyers.add(new Trade(3.0, 30));

        sellers.add(new Trade(1.0, 10));
        sellers.add(new Trade(2.0, 20));
        sellers.add(new Trade(3.0, 30));

        // 交易
        while (!buyers.isEmpty() && !sellers.isEmpty()) {
            Trade buyer = buyers.poll();
            Trade saller = sellers.poll();

            if (buyer.getPrice() >= saller.getPrice()) {
                int matchedQuantity = Math.min(buyer.getQuantity(), saller.getQuantity());
                System.out.println("交易成功，买家支付 " + buyer.getPrice() + " 元，成交 " + matchedQuantity + " 股。");

                buyer.setQuantity(buyer.getQuantity() - matchedQuantity);
                saller.setQuantity(saller.getQuantity() - matchedQuantity);
                if (buyer.getQuantity() > 0) {
                    buyers.add(buyer);
                }
                if (saller.getQuantity() > 0) {
                    sellers.add(saller);
                }
            } else {
                System.out.println("报价不匹配，交易未达成。");
            }
        }
    }
}