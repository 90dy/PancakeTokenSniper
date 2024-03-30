import * as ethers from "ethers";
import { addPool } from "../storage/pool";

// Connect to the Ethereum network
const provider = new ethers.JsonRpcProvider(
    // "https://mainnet.infura.io/v3/YOUR_INFURA_PROJECT_ID"
    // use chainstack endpoint
    "https://bsc-mainnet.core.chainstack.com/cd034565803a5dfea4b380b42debb13d"
);

// The address of the PancakeSwap V3 Factory contract
const factoryAddress = "0x0BFbCF9fa4f9C56B0F40a671Ad40E0805A091865";

// The ABI of the Factory contract
const factoryABI = [
    "event PoolCreated(address indexed token0, address indexed token1, uint24 indexed fee, int24 tickSpacing, address pool)",
];

// Create a contract instance
const factoryContract = new ethers.Contract(
    factoryAddress,
    factoryABI,
    provider
);

// Listen for the 'PoolCreated' event
factoryContract.on(
    "PoolCreated",
    (token0, token1, fee, tickSpacing, pool, event) => {
        console.log(`New pool created: ${pool}`);
        console.log(`Token0: ${token0}`);
        console.log(`Token1: ${token1}`);
        console.log(`Fee: ${fee}`);
        console.log(`TickSpacing: ${tickSpacing}`);
        // The pool contract ABI
        const poolABI = [
            "event Swap(address indexed sender, int256 amount0, int256 amount1, uint160 sqrtPriceX96, int24 tick)",
        ];
        addPool({
            address: pool,
            abi: poolABI,
            events: ["Swap"],
        });
    }
);
