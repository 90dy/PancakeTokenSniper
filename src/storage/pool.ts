type Pool = {
    // The address of the pool
    address: string;
    // The ABI of the pool
    abi: string[];
    // The events to listen for
    events: string[];
};

// The pool storage
const pools = new Map<string, Pool>();
const newPoolsQueue: Pool[][] = [];

// Add a pool to the storage
export async function addPool(pool: Pool) {
    pools.set(pool.address, pool);
    newPoolsQueue.forEach((queue) => {
        queue.push(pool);
    });
}

// Get all pools in the storage
export async function listPools() {
    return Array.from(pools.values());
}

// Get a pool by its address
export async function getPool(address: string) {
    return pools.get(address);
}

// Remove a pool from the storage
export async function removePool(address: string) {
    pools.delete(address);
}

// Remove all pools from the storage
export async function clearPools() {
    pools.clear();
}

// listen to adding queue
export async function* listenToNewPool() {
    // create a new queue
    const queue: Pool[] = [];
    newPoolsQueue.push(queue);
    while (true) {
        if (queue.length > 0) {
            const pool = queue.shift();
            if (pool) {
                yield pool;
            } else {
                break;
            }
        }
        // wait for a second
        await new Promise((resolve) => setTimeout(resolve, 1000));
    }
    // remove the queue when done
    newPoolsQueue.splice(newPoolsQueue.indexOf(queue), 1);
}
