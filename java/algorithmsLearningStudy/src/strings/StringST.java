import java.util.Collections;
import java.util.HashMap;
import java.util.Map;

import edu.princeton.cs.algs4.Queue;

/**
 * 单词查找树，不接受重复键或空键，值不能为空
 */
public class StringST {
    private Node root = new Node();
    private int size;

    private class Node {

        private Map<Character, Node> next = new HashMap<>();
        private boolean isKey;
    }

    /**
     * 创建一个符号表
     */
    public StringST() {

    }

    /**
     * 向表中插入键值对（如果值为 null，则删除键 key）
     * 
     * @param key
     * @param value
     */
    public void put(String key) {
        if (key == null) {
            throw new IllegalArgumentException("Key cannot be null");
        }
        if (contains(key)) {
            return;
        }
        root = put(root, key, 0);
        size++;
    }

    public Node put(Node x, String key, int d) {
        if (x == null) {
            x = new Node();
        }
        if (d == key.length()) {
            x.isKey = true;
            return x;
        }
        char nextChar = key.charAt(d);
        Node nextNode = put(x.next.get(nextChar), key, d + 1);
        x.next.put(nextChar, nextNode);
        return x;
    }

    // /**
    // * 键 key 所对应的值
    // *
    // * @param key
    // * @return
    // */
    // public TValue get(String key) {

    // }

    /**
     * 删除键 key
     * 
     * @param key
     */
    public void delete(String key) {
        if (key == null) {
            throw new IllegalArgumentException("Key cannot be null");
        }
        if (!contains(key)) {
            return;
        }
        root = delete(root, key, 0);
        size--;
    }

    private Node delete(Node x, String key, int d) {
        if (d == key.length()) {
            x.isKey = false;
            return x;
        } else {
            char nextChar = key.charAt(d);
            Node childNode = delete(x.next.get(nextChar), key, d + 1);
            if (childNode == null) {
                x.next.remove(nextChar);
            } else {
                x.next.put(nextChar, childNode);
            }
        }
        if (x.isKey || x.next.size() > 0) {
            return x;
        }

        return null;
    }

    /**
     * 是否存在键 key
     * 
     * @param key
     * @return
     */
    public boolean contains(String key) {
        if (key == null) {
            throw new IllegalArgumentException("Key cannot be null");
        }
        return contains(root, key, 0);
    }

    private boolean contains(Node x, String key, int d) {
        if (x == null) {
            return false;
        }
        if (d == key.length()) {
            return x.isKey;
        }

        char nextChar = key.charAt(d);
        return contains(x.next.get(nextChar), key, d + 1);
    }

    /**
     * 符号表是否为空
     * 
     * @return
     */
    public boolean isEmpty() {
        return size() == 0;
    }

    /**
     * s 的前缀中最长的键
     * 
     * @param s
     * @return
     */
    public String longestPrefixOf(String s) {

    }

    /**
     * 所有以 s 为前缀的键
     * 
     * @param s
     * @return
     */
    public Iterable<String> keysWithPrefix(String s) {

    }

    /**
     * 所有和 s 匹配的键（其中“.”能够匹配任意字符）
     * 
     * @param s
     * @return
     */
    public Iterable<String> keysThatMatch(String s) {

    }

    /**
     * 键值对的数量
     * 
     * @return
     */
    public int size() {
        return size;
    }

    /**
     * 符号表中的所有键
     * 
     * @return
     */
    public Iterable<String> keys() {
        Queue<String> keys = new Queue<>();
        keys(root, "", keys);
        return keys;
    }

    private void keys(Node x, String prefix, Queue<String> keys) {
        if (x == null) {
            return;
        }
        if (x.isKey) {
            keys.enqueue(prefix);
        }
        for (Character c : x.next.keySet()) {
            keys(x.next.get(c), new String(prefix + c), keys);
        }
    }
}
