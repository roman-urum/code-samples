import edu.princeton.cs.algs4.Digraph;
import edu.princeton.cs.algs4.Queue;

/**
 * Created by rurum on 1/18/2017.
 */
public class SAP {
    private Digraph graph;

    // constructor takes a digraph (not necessarily a DAG)
    public SAP(Digraph G){
        this.graph = G;
    }

    // length of shortest ancestral path between v and w; -1 if no such path
    public int length(int v, int w){

        ShortestAncestorInfo shortestAncestorInfo = getShortestAncestorInfo(v, w);

        if (shortestAncestorInfo == null)
        {
            return -1;
        }

        return shortestAncestorInfo.shortestPath;
    }

    // a common ancestor of v and w that participates in a shortest ancestral path; -1 if no such path
    public int ancestor(int v, int w){
        ShortestAncestorInfo shortestAncestorInfo = getShortestAncestorInfo(v, w);

        if (shortestAncestorInfo == null)
        {
            return -1;
        }

        return shortestAncestorInfo.commonAncestor;
    }

    // length of shortest ancestral path between any vertex in v and any vertex in w; -1 if no such path
    public int length(Iterable<Integer> v, Iterable<Integer> w){
        int result = -1;
        for (int i: v) {
            for (int j: w) {
                ShortestAncestorInfo shortestAncestorInfo = getShortestAncestorInfo(i,j);
                if (shortestAncestorInfo != null && shortestAncestorInfo.shortestPath < result){
                    result = shortestAncestorInfo.shortestPath;
                }
            }
        }
        return result;
    }

    // a common ancestor that participates in shortest ancestral path; -1 if no such path
    public int ancestor(Iterable<Integer> v, Iterable<Integer> w){
        int result = -1;
        for (int i: v) {
            for (int j: w) {
                ShortestAncestorInfo shortestAncestorInfo = getShortestAncestorInfo(i,j);
                if (shortestAncestorInfo != null && shortestAncestorInfo.shortestPath < result){
                    result = shortestAncestorInfo.shortestPath;
                }
            }
        }
        return result;
    }

    // do unit testing of this class
    public static void main(String[] args){

    }

    private ShortestAncestorInfo getShortestAncestorInfo(int v, int w){
        boolean[] vMarked = new boolean[graph.V()];
        vMarked[v] = true;
        int[] vDistTo = new int[graph.V()];
        vDistTo[v] = 0;
        Queue<Integer> queue = new Queue<>();
        queue.enqueue(v);
        while(!queue.isEmpty()){
            int i = queue.dequeue();
            for (int j: graph.adj(i)){
                if (!vMarked[j]){
                    vDistTo[j] = vDistTo[i] + 1;
                    vMarked[j] = true;
                    queue.enqueue(j);
                }
            }
        }

        boolean[] wMarked = new boolean[graph.V()];
        vMarked[w] = true;
        int[] wDistTo = new int[graph.V()];
        wDistTo[w] = 0;
        queue = new Queue<>();
        queue.enqueue(w);
        while(!queue.isEmpty()){
            int i = queue.dequeue();

            if (vMarked[i]) {
                return new ShortestAncestorInfo(vDistTo[i] + wDistTo[i], i);
            }

            for (int j : graph.adj(i)) {
                if (!wMarked[j]){
                    wDistTo[j] = wDistTo[i] + 1;
                    wMarked[j] = true;
                    queue.enqueue(j);
                }
            }
        }

        return null;
    }

    private class ShortestAncestorInfo{
        public int shortestPath;
        public int commonAncestor;

        public ShortestAncestorInfo(int shortestPath, int commonAncestor){
            this.shortestPath = shortestPath;
            this.commonAncestor = commonAncestor;
        }
    }
}
