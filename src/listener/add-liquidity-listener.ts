import { ethers } from "ethers";
import { listenToNewPool } from "../storage/pool";

// Example: Listening to the Mint event on a specific Uniswap V3 Pool

// Initialize provider (Infura, Alchemy, or other node providers)
const provider = new ethers.JsonRpcProvider("YOUR_RPC_ENDPOINT");

for await (const pool of listenToNewPool()) {
    console.log(`Listening for liquidity additions to pool: ${pool.address}`);

    // Create an instance of the pool contract
    const poolContract = new ethers.Contract(pool.address, pool.abi, provider);

    // Listen for the Mint event
    poolContract.on(
        "Mint",
        (sender, owner, tickLower, tickUpper, amount, amount0, amount1) => {
            console.log(`Liquidity added by ${owner}`);
            console.log(
                `Amount: ${amount}, Amount0: ${amount0.toString()}, Amount1: ${amount1.toString()}`
            );
            // Additional logic to handle the event can be added here
        }
    );

    console.log(`Listening for liquidity additions to pool: ${pool.address}`);
}
