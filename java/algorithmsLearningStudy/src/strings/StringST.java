/**
 * 单词查找树，不接受重复键或空键，值不能为空
 */
public class StringST<TValue> {
    /**
     * 创建一个符号表
     */
    public StringST() {

    }

    /**
     * 向表中插入键值对（如果值为 null，则删除键 key）
     * @param key
     * @param value
     */
    public void put(String key, TValue value) {

    }

    /**
     * 键 key 所对应的值
     * @param key
     * @return
     */
    public TValue get(String key) {

    }

    /**
     * 删除键 key
     * @param key
     */
    public void delete(String key) {

    }

    /**
     * 是否存在键 key
     * @param key
     * @return
     */
    public boolean contains(String key) {

    }

    /**
     * 符号表是否为空
     * @return
     */
    public boolean isEmpty() {

    }

    /**
     * s 的前缀中最长的键
     * @param s
     * @return
     */
    public String longestPrefixOf(String s) {

    }

    /**
     * 所有以 s 为前缀的键
     * @param s
     * @return
     */
    public Iterable<String> keysWithPrefix(String s) {

    }

    /**
     * 所有和 s 匹配的键（其中“.”能够匹配任意字符）
     * @param s
     * @return
     */
    public Iterable<String> keysThatMatch(String s) {

    }

    /**
     * 键值对的数量
     * @return
     */
    public int size() {

    }

    /**
     * 符号表中的所有键
     * @return
     */
    public Iterable<String> keys() {

    }
}
